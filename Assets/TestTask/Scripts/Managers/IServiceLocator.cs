using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public interface IServiceLocator
    {
        T Get<T>();
        void Add<T>(T provider);
    }
}
