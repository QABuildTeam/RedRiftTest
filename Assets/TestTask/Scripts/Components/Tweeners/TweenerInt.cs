using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class TweenerInt : Tweener<int>
    {
        protected override void SetNextStep()
        {
            step = newValue > currentValue ? tweeningSpeed : -tweeningSpeed;
            current = currentValue;
            end = newValue;
        }

        protected override void StepToNextState()
        {
            base.StepToNextState();
            currentValue = (int)current;
        }
    }
}
