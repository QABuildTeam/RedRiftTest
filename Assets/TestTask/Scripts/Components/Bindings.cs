using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public interface IVisualizer<T>
    {
        void Show(T value);
    }

    public class VisualValue<T>
    {
        protected T value;
        protected List<IVisualizer<T>> visualizers = new List<IVisualizer<T>>();

        public VisualValue<T> Clone()
        {
            return new VisualValue<T> { value = value };
        }

        public static implicit operator VisualValue<T>(T value) => new VisualValue<T> { value = value };
        public override string ToString() => value.ToString();

        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                foreach (var visualizer in visualizers)
                {
                    visualizer.Show(value);
                }
            }
        }

        public VisualValue<T> Bind(IVisualizer<T> visualizer)
        {
            if (!visualizers.Contains(visualizer))
            {
                visualizers.Add(visualizer);
            }
            return this;
        }

        public VisualValue<T> Unbind(IVisualizer<T> visualizer)
        {
            if (visualizers.Contains(visualizer))
            {
                visualizers.Remove(visualizer);
            }
            return this;
        }

        public VisualValue<T> Display()
        {
            Value = Value;
            return this;
        }
    }
}
