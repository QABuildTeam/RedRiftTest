using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class UniversalObjectPoolComponent : ServiceProvider
    {
        public override void RegisterService(IServiceLocator locator)
        {
            if (locator.Get<UniversalObjectPool>() == null)
            {
                locator.Add(new UniversalObjectPool());
            }
        }
    }
}
