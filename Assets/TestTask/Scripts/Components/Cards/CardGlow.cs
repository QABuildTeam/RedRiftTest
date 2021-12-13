using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TestTask
{
    public class CardGlow : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        private UniversalSOProvider SOProvider => GlobalManager.Instance.Get<UniversalSOProvider>();

        [SerializeField]
        private Image glow;

        public void OnBeginDrag(PointerEventData eventData)
        {
            // poor man's glow
            Color c = glow.color;
            c.a = SOProvider.Get<CardSettings>().glowIntensity;
            glow.color = c;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Color c = glow.color;
            c.a = 0;
            glow.color = c;
        }
    }
}
