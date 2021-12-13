using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    /// <summary>
    /// Global singleton for all game services
    /// </summary>
    public class GlobalManager : MonoBehaviour,IServiceLocator
    {
        public static GlobalManager Instance { get; private set; }

        private Dictionary<System.Type, object> providers = new Dictionary<System.Type, object>();

        public bool Initialized { get; private set; } = false;
        public T Get<T>()
        {
            System.Type type = typeof(T);
            if (providers.ContainsKey(type))
            {
                return (T)providers[type];
            }
            return default;
        }

        public void Add<T>(T provider)
        {
            System.Type type = typeof(T);
            if (!providers.ContainsKey(type))
            {
                providers.Add(type, provider);
            }
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                RegisterServices();
                Initialized = true;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void RegisterServices()
        {
            // all ServiceProvider components must reside in the very same GameObject where MCGlobal belongs
            foreach (var service in GetComponents<ServiceProvider>())
            {
                if (!service.Disabled)
                {
                    service.RegisterService(this);
                }
            }
        }
    }
}
