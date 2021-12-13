using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class LocalZRotationVisualizer : IVisualizer<float>
    {
        public Transform transform;
        public void Show(float zAngle)
        {
            Debug.Log($"[{GetType().Name}.{nameof(Show)}] zAngle={zAngle}");
            Quaternion q = transform.localRotation;
            Vector3 ea = q.eulerAngles;
            q.eulerAngles = new Vector3(ea.x, ea.y, zAngle);
            transform.localRotation = q;
        }
    }
}
