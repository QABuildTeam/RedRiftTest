using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public abstract class Tweener<T>
    {
        protected T currentValue;
        protected T newValue;
        protected MonoBehaviour master;
        protected IVisualizer<T> visualizer;

        protected float current;
        protected float end;
        protected float tweeningSpeed;
        protected float step;
        protected Action<T> action;

        private Coroutine tweenerCoroutine = null;

        public Tweener<T> Init(T initialValue, IVisualizer<T> visualizer)
        {
            currentValue = initialValue;
            this.master = GlobalManager.Instance;
            this.visualizer = visualizer;
            return this;
        }

        public Tweener<T> SetSpeed(float speed)
        {
            tweeningSpeed = Mathf.Abs(speed);
            return this;
        }

        public Tweener<T> SetAction(Action<T> postAction)
        {
            action = postAction;
            return this;
        }

        public Tweener<T> To(T endValue)
        {
            newValue = endValue;
            SetNextStep();
            if (tweenerCoroutine == null)
            {
                tweenerCoroutine = master.StartCoroutine(TweenerCoroutine());
            }
            return this;
        }

        protected abstract void SetNextStep();

        protected virtual void VisualizeState()
        {
            visualizer.Show(currentValue);
        }

        protected virtual bool CheckEndState()
        {
            return (step > 0 && current < end) || (step < 0 && current > end);
        }

        protected virtual void StepToNextState()
        {
            current += step * Time.deltaTime;
        }

        private IEnumerator TweenerCoroutine()
        {
            while (CheckEndState())
            {
                VisualizeState();
                yield return null;
                StepToNextState();
            }
            currentValue = newValue;
            VisualizeState();
            tweenerCoroutine = null;
            action?.Invoke(newValue);
        }
    }
}
