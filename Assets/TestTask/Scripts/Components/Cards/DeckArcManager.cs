using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TestTask
{
    public class DeckArcManager : MonoBehaviour
    {
        private UniversalObjectPool ObjectPool => GlobalManager.Instance.Get<UniversalObjectPool>();
        private UniversalEventManager EventManager => GlobalManager.Instance.Get<UniversalEventManager>();
        private UniversalSOProvider SOProvider => GlobalManager.Instance.Get<UniversalSOProvider>();

        [SerializeField]
        private RectTransform managerPoint;
        [SerializeField]
        private CardRotator cardRotatorPrefab;
        [SerializeField]
        private UICard uiCardPrefab;

        private class CardCell
        {
            public CardRotator center;
            public UICard uiCard;
            public Vector3 cardPosition;
        }

        private List<CardCell> cardCells = new List<CardCell>();

        private void Awake()
        {
            EventManager.Get<CardEvents>().AddCard += OnAddCard;
            EventManager.Get<CardEvents>().RemoveCard += OnRemoveCard;
            EventManager.Get<CardEvents>().DetachCard += OnDetachCard;
            EventManager.Get<CardEvents>().ReattachCard += OnReattachCard;
            EventManager.Get<CardEvents>().BeginDrag += OnBeginDrag;
        }

        private void OnAddCard(Card card, Action<int> attackAction, Action<int> hpAction, Action<int> mpAction)
        {
            Add(card, attackAction, hpAction, mpAction);
        }

        private void OnRemoveCard(Card card)
        {
            Remove(card);
        }

        private void OnDetachCard(Card card)
        {
            Detach(card);
        }

        private void OnReattachCard(Card card)
        {
            Reattach(card);
        }

        public void Add(Card card, Action<int> attackAction, Action<int> hpAction, Action<int> mpAction)
        {
            float arcRadius = SOProvider.Get<DeckArcSettings>().arcRadius;
            CardCell cell = new CardCell
            {
                center = ObjectPool.GetObject<CardRotator>(cardRotatorPrefab),
                uiCard = ObjectPool.GetObject<UICard>(uiCardPrefab)
            };
            cell.center.Center.SetParent(managerPoint);
            cell.center.Center.localPosition = Vector3.zero;
            cell.uiCard.transform.SetParent(cell.center.Center);
            cell.uiCard.transform.localPosition = new Vector3(0, arcRadius, 0);
            cell.uiCard.Setup(card, attackAction, hpAction, mpAction);
            cardCells.Add(cell);
            PlaceCards();
        }

        private CardCell FindUICard(Card card)
        {
            // poor man's LINQ
            foreach (var cell in cardCells)
            {
                if (cell.uiCard.TrackableItem == card)
                {
                    return cell;
                }
            }
            return null;
        }

        public void Remove(Card card)
        {
            var cell = FindUICard(card);
            if (cell != null)
            {
                ObjectPool.PutObject(cell.uiCard);
                ObjectPool.PutObject(cell.center);
                cardCells.Remove(cell);
                PlaceCards();
            }
        }

        public UICard Detach(Card card)
        {
            UICard uiCard = null;
            var cell = FindUICard(card);
            if (cell != null)
            {
                // the card is already detached now
                uiCard = cell.uiCard;
                ObjectPool.PutObject(cell.center);
                cardCells.Remove(cell);
                PlaceCards();
            }
            return uiCard;
        }

        private void OnBeginDrag(Transform transform, Vector3 position)
        {
            var uiCard = transform.GetComponent<UICard>();
            if (uiCard != null)
            {
                var cell = FindUICard(uiCard.TrackableItem);
                if (cell != null)
                {
                    cell.cardPosition = transform.position;
                }
            }
        }

        public void Reattach(Card card)
        {
            var cell = FindUICard(card);
            if (cell != null)
            {
                var cardTransform = cell.uiCard.transform;
                cardTransform.SetParent(cell.center.Center, worldPositionStays: true);
                var staticVisualizer = new PositionVisualizer() { transform = cardTransform };
                var tweener = new TweenerVector3().Init(cardTransform.position, staticVisualizer).SetSpeed(SOProvider.Get<DeckArcSettings>().cardReturnSpeed);
                var visualizer = new TweenerVisualizer<Vector3>().Init(tweener);
                var value = new VisualValue<Vector3> { Value = cardTransform.position };
                value.Bind(visualizer);
                value.Value = cell.cardPosition;
            }
        }

        protected void PlaceCards()
        {
            if (cardCells.Count > 0)
            {
                float arcAngle = SOProvider.Get<DeckArcSettings>().deckArcAngle;
                float angle = -arcAngle / 2;
                float deltaAngle = arcAngle / cardCells.Count;
                for (int i = 0; i < cardCells.Count; ++i, angle += deltaAngle)
                {
                    float cardAngle = angle + deltaAngle / 2;
                    var pair = cardCells[i];
                    pair.center.SetZAngle(-cardAngle);
                }
            }
        }
    }
}
