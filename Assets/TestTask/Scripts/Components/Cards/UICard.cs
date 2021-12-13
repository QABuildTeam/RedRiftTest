using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TestTask
{
    public class UICard : ObjectPoolTemplate
    {
        private UniversalSOProvider SOProvider => GlobalManager.Instance.Get<UniversalSOProvider>();
        private CardSettings Settings => SOProvider.Get<CardSettings>();

        [Header("Static fields")]
        [SerializeField]
        private TextMeshProUGUI titleText;
        [SerializeField]
        private TextMeshProUGUI descriptionText;
        [Header("Visual values")]
        [SerializeField]
        private CardStat attackStat;
        [SerializeField]
        private CardStat hpStat;
        [SerializeField]
        private CardStat mpStat;

        public Card TrackableItem { get; protected set; } = null;

        private TweenerVisualizer<int> attackVisualizer = null;
        private TweenerVisualizer<int> hpVisualizer = null;
        private TweenerVisualizer<int> mpVisualizer = null;

        public override void Decease()
        {
            UnbindAll();
            base.Decease();
        }

        private TweenerVisualizer<int> SetupTweenerVisualizerInt(TweenerVisualizer<int> visualizer, CardStat stat, float tweenerSpeed, Action<int> action)
        {
            if (visualizer == null)
            {
                var textVisualizer = stat.GetVisualizer<int>();
                var tweener = new TweenerInt().Init(0, textVisualizer).SetSpeed(tweenerSpeed).SetAction(action);
                visualizer = new TweenerVisualizer<int>().Init(tweener);
            }
            return visualizer;
        }

        private void BindAll(Action<int> attackAction = null, Action<int> hpAction = null, Action<int> mpAction = null)
        {
            if (TrackableItem != null)
            {
                TrackableItem.attack.Bind(attackVisualizer = SetupTweenerVisualizerInt(attackVisualizer, attackStat, Settings.statAnimationSpeed, attackAction)).Display();
                TrackableItem.hp.Bind(hpVisualizer = SetupTweenerVisualizerInt(hpVisualizer, hpStat, Settings.statAnimationSpeed, hpAction)).Display();
                TrackableItem.mp.Bind(mpVisualizer = SetupTweenerVisualizerInt(mpVisualizer, mpStat, Settings.statAnimationSpeed, mpAction)).Display();
            }
        }

        private void UnbindAll()
        {
            if (TrackableItem != null)
            {
                TrackableItem.attack.Unbind(attackVisualizer);
                TrackableItem.hp.Unbind(hpVisualizer);
                TrackableItem.mp.Unbind(mpVisualizer);
                TrackableItem = null;
            }
        }

        public void Setup(Card card, Action<int> attackAction = null, Action<int> hpAction = null, Action<int> mpAction = null)
        {
            UnbindAll();
            TrackableItem = card;
            if (TrackableItem != null)
            {
                titleText.text = TrackableItem.title.Value;
                descriptionText.text = TrackableItem.description.Value;
            }
            BindAll(attackAction, hpAction, mpAction);
        }
    }
}
