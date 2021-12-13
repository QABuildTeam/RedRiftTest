using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TestTask
{
    /// <summary>
    /// Simplistic text visualizer
    /// </summary>
    /// <typeparam name="T">A type of the value to visualize</typeparam>
    public class TextVisualizer<T> : IVisualizer<T>
    {
        public TextMeshProUGUI text;

        public void Show(T value)
        {
            if (text != null)
            {
                text.text = value.ToString();
            }
        }
    }
}
