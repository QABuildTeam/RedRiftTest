using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TestTask
{
    public class CancelDropArea : MonoBehaviour, IDropHandler
    {
        private UniversalEventManager EventManager => GlobalManager.Instance.Get<UniversalEventManager>();

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData != null && eventData.pointerDrag != null)
            {
                var uiCard = eventData.pointerDrag.GetComponent<UICard>();
                if (uiCard != null)
                {
                    var dragger = eventData.pointerDrag.GetComponent<CardDragger>();
                    dragger.Interactable = true;
                    EventManager.Get<CardEvents>().ReattachCard?.Invoke(uiCard.TrackableItem);
                }
            }
        }
    }
}
