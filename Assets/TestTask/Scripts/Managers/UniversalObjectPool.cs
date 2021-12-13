using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    /// <summary>
    /// A pool of objects of type T
    /// </summary>
    /// <typeparam name="T">The type of pool objects</typeparam>
    public class GenericObjectPool<T> where T : ObjectPoolTemplate
    {
        private readonly Stack<T> inactiveObjects = new Stack<T>();
        public T GetObject(T prefab, params object[] args)
        {
            T recycledObject;
            if (inactiveObjects.Count == 0)
            {
                recycledObject = (T)GameObject.Instantiate(prefab);
                recycledObject.Init(args);
            }
            else
            {
                recycledObject = inactiveObjects.Pop();
            }
            recycledObject.Revive();
            return recycledObject;
        }

        public void PutObject(T recycledObject)
        {
            recycledObject.Decease();
            if (!inactiveObjects.Contains(recycledObject) && recycledObject != null)
            {
                inactiveObjects.Push(recycledObject);
            }
        }
    }

    /// <summary>
    /// A universal object pool factory: it creates GenericObjectPools of given type using a list of predefined object pool creators
    /// </summary>
    public class UniversalObjectPool
    {
        private readonly Dictionary<Type, object> pools = new Dictionary<Type, object>();

        protected GenericObjectPool<T> GetPool<T>() where T : ObjectPoolTemplate
        {
            Type type = typeof(T);
            GenericObjectPool<T> pool;
            if (!pools.ContainsKey(type))
            {
                pool = new GenericObjectPool<T>();
                if (pool != null)
                {
                    pools.Add(type, pool);
                }
            }
            else
            {
                pool = (GenericObjectPool<T>)pools[type];
            }
            return pool;
        }

        public T GetObject<T>(T prefab, params object[] args) where T : ObjectPoolTemplate
        {
            GenericObjectPool<T> pool = GetPool<T>();
            return pool.GetObject(prefab, args);
        }

        public void PutObject<T>(T obj) where T : ObjectPoolTemplate
        {
            if (obj != null)
            {
                GenericObjectPool<T> pool = GetPool<T>();
                pool.PutObject(obj);
            }
        }
    }
}
