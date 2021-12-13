using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class UniversalEventManagerComponent : ServiceProvider
    {
        public override void RegisterService(IServiceLocator locator)
        {
            if (locator.Get<UniversalEventManager>() == null)
            {
                locator.Add(new UniversalEventManager());
                // UniversalEventManager does not need registering additional components
            }
        }
    }
}
