using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    /// <summary>
    /// This class may be used as a base class for ObjectPool templates
    /// </summary>
    public class ObjectPoolTemplate : MonoBehaviour
    {
        public virtual void Init(params object[] args)
        {
            // create all needed accessories
        }

        public virtual void Revive()
        {
            gameObject.SetActive(true);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        public virtual void Decease()
        {
            gameObject.SetActive(false);
            transform.SetParent(null);
        }
    }
}
