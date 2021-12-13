using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class TweenerVector3 : Tweener<Vector3>
    {
        private Vector3 vectorStep;

        protected override void SetNextStep()
        {
            current = 0;
            end = (newValue - currentValue).magnitude;
            step = tweeningSpeed;
            vectorStep = (newValue - currentValue).normalized * tweeningSpeed;
            Debug.Log($"[{GetType().Name}.{nameof(SetNextStep)}] newValue={newValue}, currentValue={currentValue}, vectorStep={vectorStep}");
        }

        protected override void StepToNextState()
        {
            base.StepToNextState();
            currentValue += vectorStep * Time.deltaTime;
        }
    }
}
