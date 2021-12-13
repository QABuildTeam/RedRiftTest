using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestTask
{
    public class UniversalSOProvider
    {
        protected Dictionary<Type, SettingsTemplate> soHub = new Dictionary<Type, SettingsTemplate>();

        public void Add(SettingsTemplate so)
        {
            if (so != null)
            {
                Type type = so.GetType();
                if (!soHub.ContainsKey(type))
                {
                    soHub.Add(type, so);
                }
            }
        }

        public T Get<T>() where T : SettingsTemplate
        {
            Type type = typeof(T);
            if (soHub.ContainsKey(type))
            {
                return (T)soHub[type];
            }
            return default;
        }
    }
}
