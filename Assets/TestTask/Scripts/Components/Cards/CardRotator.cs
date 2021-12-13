using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class CardRotator : ObjectPoolTemplate
    {
        private UniversalSOProvider SOProvider => GlobalManager.Instance.Get<UniversalSOProvider>();

        public Transform Center { get; private set; }

        public override void Init(params object[] args)
        {
            base.Init(args);
            Center = GetComponent<Transform>();
            var staticVisualizer = new LocalZRotationVisualizer() { transform = Center };
            var tweener = new TweenerFloat().Init(0, staticVisualizer).SetSpeed(SOProvider.Get<DeckArcSettings>().rotationSpeed);
            visualizer = new TweenerVisualizer<float>().Init(tweener);
        }

        protected VisualValue<float> zAngle = new VisualValue<float> { Value = 0 };
        protected TweenerVisualizer<float> visualizer;

        public override void Revive()
        {
            base.Revive();
            zAngle.Bind(visualizer);
        }
        public override void Decease()
        {
            zAngle.Unbind(visualizer);
            base.Decease();
        }

        public void SetZAngle(float angle, Action<float> postAction = null)
        {
            visualizer.Tweener.SetAction(postAction);
            zAngle.Value = angle;
        }
    }
}
