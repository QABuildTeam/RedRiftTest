using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TestTask
{
    public class CardDragger : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private UniversalEventManager EventManager => GlobalManager.Instance.Get<UniversalEventManager>();

        [SerializeField]
        private Image colliderImage;
        private Image ColliderImage
        {
            get
            {
                return colliderImage;
            }
        }

        public bool Interactable
        {
            get
            {
                return ColliderImage.raycastTarget;
            }
            set
            {
                ColliderImage.raycastTarget = value;
            }
        }

        private Transform selfTransform;
        private Transform SelfTransform
        {
            get
            {
                if (selfTransform == null)
                {
                    selfTransform = GetComponent<Transform>();
                }
                return selfTransform;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (Interactable)
            {
                Interactable = false;
                EventManager.Get<CardEvents>().BeginDrag?.Invoke(SelfTransform, eventData.position);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            EventManager.Get<CardEvents>().Drag?.Invoke(SelfTransform, eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EventManager.Get<CardEvents>().EndDrag?.Invoke(SelfTransform, eventData.position);
        }
    }
}
