using System;
using System.Collections.Generic;
using UnityEngine;

namespace MatchGame.Core
{
    public class ServiceLocator : CoreSystem
    {
        private static Dictionary<Type, object> serviceRegistry = new Dictionary<Type, object>();
        private static ServiceLocator instance;

        public static ServiceLocator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceLocator();
                }
                return instance;
            }
        }

        // Register a service with the service locator
        public void Register<T>(T service)
        {
            Type serviceType = typeof(T);
            if (!serviceRegistry.ContainsKey(serviceType))
            {
                serviceRegistry.Add(serviceType, service);
            }
        }

        // Get a service from the service locator
        public T Get<T>()
        {
            Type serviceType = typeof(T);
            if (serviceRegistry.TryGetValue(serviceType, out object service))
            {
                return (T)service;
            }
            else
            {
                Debug.LogError("Service not found: " + serviceType.Name);
                return default(T);
            }
        }
    }
}
