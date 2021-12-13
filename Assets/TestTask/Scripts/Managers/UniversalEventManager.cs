using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    #region GEvent generic classes definitions
    public delegate void GEvent();

    public delegate void GEvent<T1>(T1 arg1);

    public delegate void GEvent<T1, T2>(T1 arg1, T2 arg2);

    public delegate void GEvent<T1, T2, T3>(T1 arg1, T2 arg2, T3 arg3);

    public delegate void GEvent<T1, T2, T3, T4>(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

    public delegate void GEvent<T1, T2, T3, T4, T5>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);
    #endregion

    public interface IEventProvider { }

    public class UniversalEventManager
    {
        protected Dictionary<System.Type, IEventProvider> eventHub = new Dictionary<System.Type, IEventProvider>();

        public void Add(IEventProvider eventProvider)
        {
            System.Type type = eventProvider.GetType();
            if (!eventHub.ContainsKey(type))
            {
                eventHub.Add(type, eventProvider);
            }
            else
            {
                Debug.LogError($"[{nameof(UniversalEventManager)}.{nameof(Add)} Event provider of type {type.Name} is already registered");
            }
        }

        public void Remove(IEventProvider eventProvider)
        {
            eventHub.Remove(eventProvider.GetType());
        }

        public T Get<T>() where T : IEventProvider, new()
        {
            System.Type type = typeof(T);
            if (eventHub.ContainsKey(type))
            {
                return (T)eventHub[type];
            }
            else
            {
                T eventProvider = new T();
                eventHub.Add(type, eventProvider);
                return eventProvider;
            }
        }

    }
}
