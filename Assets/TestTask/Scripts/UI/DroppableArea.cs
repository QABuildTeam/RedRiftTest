using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestTask
{
    public class DroppableArea : MonoBehaviour, IDropHandler
    {
        private UniversalEventManager EventManager => GlobalManager.Instance.Get<UniversalEventManager>();

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData != null && eventData.pointerDrag != null)
            {
                var dragger = eventData.pointerDrag.GetComponent<CardDragger>();
                if (dragger != null)
                {
                    // keep here
                    eventData.pointerDrag.transform.SetParent(transform, worldPositionStays: true);
                    dragger.Interactable = false;
                    var uiCard = eventData.pointerDrag.GetComponent<UICard>();
                    if (uiCard != null)
                    {
                        EventManager.Get<CardEvents>().DetachCard?.Invoke(uiCard.TrackableItem);
                    }
                }
            }
        }
    }
}
