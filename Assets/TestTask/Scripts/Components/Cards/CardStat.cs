using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TestTask
{
    public class CardStat : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI textValue;

        public TextVisualizer<T> GetVisualizer<T>()
        {
            return new TextVisualizer<T>
            {
                text = textValue
            };
        }
    }
}
