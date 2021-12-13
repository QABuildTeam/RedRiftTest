using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public abstract class ServiceProvider : MonoBehaviour
    {
        [SerializeField]
        private bool disabled = false;
        public virtual bool Disabled => disabled;

        public abstract void RegisterService(IServiceLocator locator);
    }
}
