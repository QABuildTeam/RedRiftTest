using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class PositionVisualizer : IVisualizer<Vector3>
    {
        public Transform transform;

        public void Show(Vector3 value)
        {
            transform.position = value;
        }
    }
}
