using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class GameManager : MonoBehaviour
    {
        private UniversalSOProvider SOProvider => GlobalManager.Instance.Get<UniversalSOProvider>();
        private UniversalEventManager EventManager => GlobalManager.Instance.Get<UniversalEventManager>();

        private class CardPlay
        {
            public Card card;
            public List<VisualValue<int>> values;
        }

        private List<CardPlay> cards;

        private void Awake()
        {
            EventManager.Get<UIEvents>().PlayerButtonPressed += OnPlayerButtonPressed;
            EventManager.Get<CardEvents>().DetachCard += OnDetachCard;
        }

        private void OnPlayerButtonPressed()
        {
            PlayRandomCard();
        }

        private void OnDetachCard(Card card)
        {
            CheckDetachedCard(card);
        }

        void Start()
        {
            Init();
        }

        private int playIndex = 0;


        private void Init()
        {
            var gameSettings = SOProvider.Get<GameSettings>();
            int cardsCount = Random.Range(gameSettings.minCardsInDeck, gameSettings.maxCardsInDeck + 1);
            var cardData = SOProvider.Get<CardSettings>().cardData;
            cards = new List<CardPlay>();
            for (int i = 0; i < cardsCount; ++i)
            {
                int cardDataIndex = Random.Range(0, cardData.Count);
                var card = cardData[cardDataIndex].Convert();
                var cardPlay = new CardPlay
                {
                    card = card,
                    values = new List<VisualValue<int>>
                    {
                        card.attack,
                        card.hp,
                        card.mp
                    }
                };
                cards.Add(cardPlay);
                EventManager.Get<CardEvents>().AddCard?.Invoke(card, null, (value) => RemoveCardOnLowHP(card, value), null);
            }
        }

        private void PlayRandomCard()
        {
            if (cards.Count > 0)
            {
                if (playIndex >= 0 && playIndex < cards.Count)
                {
                    Debug.Log($"[{GetType().Name}.{nameof(PlayRandomCard)}] Playing card {playIndex}");
                    var gameSettings = SOProvider.Get<GameSettings>();
                    int propertyIndex = Random.Range(0, cards[playIndex].values.Count);
                    int randomValue = Random.Range(gameSettings.minPropertyValue, gameSettings.maxPropertyValue + 1);

                    cards[playIndex].values[propertyIndex].Value = randomValue;
                }
                playIndex = (playIndex + 1) % cards.Count;
                Debug.Log($"[{GetType().Name}.{nameof(PlayRandomCard)}] Next card {playIndex}");
            }
        }

        private CardPlay FindCard(Card card)
        {
            foreach (var cardPlay in cards)
            {
                if (cardPlay.card == card)
                {
                    return cardPlay;
                }
            }
            return null;
        }

        private void RemoveCardOnLowHP(Card card, int hp)
        {
            var gameSettings = SOProvider.Get<GameSettings>();
            if (hp < gameSettings.lowHPValue)
            {
                //Debug.Log($"[{GetType().Name}.{nameof(RemoveCardOnLowHP)}] Removing card {card}, hp={hp}");
                EventManager.Get<CardEvents>().RemoveCard?.Invoke(card);
                var cardToRemove = FindCard(card);
                if (cardToRemove != null)
                {
                    cards.Remove(cardToRemove);
                    playIndex %= cards.Count;
                }
            }
        }

        private void CheckDetachedCard(Card card)
        {
            var cardPlay = FindCard(card);
            if (cardPlay != null)
            {
                cards.Remove(cardPlay);
                playIndex %= cards.Count;
            }
        }
    }
}
