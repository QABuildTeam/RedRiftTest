using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class TweenerVisualizer<T> : IVisualizer<T>
    {
        public Tweener<T> Tweener { get; set; }
        protected Action<T> action;

        public TweenerVisualizer<T> Init(Tweener<T> tweener)
        {
            Tweener = tweener;
            return this;
        }

        public void Show(T value)
        {
            Tweener.To(value);
        }
    }
}
