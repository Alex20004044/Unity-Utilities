using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UniRx;
using UnityEngine;

namespace MSFD
{
    /// <summary>
    /// Service Locator. Restrictions: Simultaneous existence of Factory and Service with a same target type is not allowed. Not interface type registration is not allowed
    /// </summary>
    public static class ServiceLocator
    {
        static Dictionary<Type, object> services = new Dictionary<Type, object>();
        static Dictionary<Type, Func<object>> factories = new Dictionary<Type, Func<object>>();
        public static void Register<T>(T service) where T : class
        {
            if(!TryRegister<T>(service)) throw new ArgumentException($"{typeof(T)} already registered in ServiceLocator");
        }
        public static bool TryRegister<T>(T service) where T : class
        {
            //#if DEBUG
            if (service == null) throw new NullReferenceException("Attempt to register null service");
            if (!typeof(T).IsInterface) throw new InvalidCastException("Attempt to register not interface type. T: " + typeof(T).ToString());
            if (factories.ContainsKey(typeof(T))) return false;
            //#endif
            return services.TryAdd(typeof(T), service);
        }

        public static void RegisterFactory<T>(Func<T> factory) where T : class
        {
            if (!TryRegisterFactory<T>(factory)) throw new ArgumentException($"{typeof(T)} already registered in ServiceLocator");
        }
        public static bool TryRegisterFactory<T>(Func<T> factory) where T: class
        {
            if (factory == null) throw new NullReferenceException("Attempt to register null factory");
            if (!typeof(T).IsInterface) throw new InvalidCastException("Attempt to register not interface type. T: " + typeof(T).ToString());
            if (services.ContainsKey(typeof(T))) return false;

            return factories.TryAdd(typeof(T), factory);
        }
        public static T Get<T>() where T : class
        {
            if (TryGet<T>(out var value)) return value;
            throw new KeyNotFoundException($"Type {typeof(T)} is not registered in ServiceLocator");
        }        
        public static bool TryGet<T>(out T value) where T : class
        {
            if (!typeof(T).IsInterface) throw new InvalidCastException("Attempt to get not interface type. T: " + typeof(T).ToString());
            if (services.TryGetValue(typeof(T), out object service))
            {
                value = (T)service;
                return true;
            }
            if (factories.TryGetValue(typeof(T), out var factory))
            {
                value = (T)factory.Invoke();
                return true; 
            }
            value = null;
            return false;
        }

        public static bool IsTypeRegistered<T>() where T : class
        {
            return services.ContainsKey(typeof(T)) || factories.ContainsKey(typeof(T));
        }
        public static bool TryUnregisterType<T>() where T : class
        {
            return services.Remove(typeof(T)) || factories.Remove(typeof(T));
        }
        /// <summary>
        /// Register service if factory or service have not been registered or set new service otherwise and delete previous factory or service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void Set<T>(T service) where T : class
        {
            factories.Remove(typeof(T));
            services[typeof(T)] = service;
/*            TryUnregisterType<T>();
            TryRegister<T>(service);*/
        }
        /// <summary>
        /// Register factory if factory or service have not been registered or set new factory otherwise and delete previous factory or service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static void SetFactory<T>(Func<T> factory) where T : class
        {
            services.Remove(typeof(T));
            factories[typeof(T)] = factory;
            /*TryUnregisterType<T>();
            TryRegisterFactory<T>(factory);*/
        }

        /// <summary>
        /// Delete all services and factories
        /// </summary>
        public static void Clear()
        {
            services.Clear();
            factories.Clear();
        }
        public static IEnumerable<KeyValuePair<Type, object>> GetServicesEnumerator()
        {
            return services;
        }
        public static IEnumerable<KeyValuePair<Type, Func<object>>> GetFactoriesEnumerator()
        {
            return factories;
        }
    }
}