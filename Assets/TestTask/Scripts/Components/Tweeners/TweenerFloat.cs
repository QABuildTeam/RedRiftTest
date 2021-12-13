using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class TweenerFloat : Tweener<float>
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
            currentValue += step * Time.deltaTime;
        }
    }
}
