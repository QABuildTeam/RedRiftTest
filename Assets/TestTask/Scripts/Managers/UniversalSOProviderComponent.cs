using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class UniversalSOProviderComponent : ServiceProvider
    {
        [SerializeField]
        private SettingsTemplate[] assets;
        public override void RegisterService(IServiceLocator locator)
        {
            if (locator.Get<UniversalSOProvider>() == null)
            {
                UniversalSOProvider scriptableObjectProvider = new UniversalSOProvider();
                foreach (var asset in assets)
                {
                    if (asset != null && !asset.disabled)
                    {
                        scriptableObjectProvider.Add(asset);
                    }
                }
                locator.Add(scriptableObjectProvider);
            }
        }
    }
}
