using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Utilities
{
    public class InjectionLoader<T>
    {
        private Type[] injectableTypes;
        
        private Type[] GetInjectableTypes() 
        {
            if (injectableTypes == null)
            {
                injectableTypes = TypeCache.GetTypesDerivedFrom<T>()
                                            .ToArray();
            }
            return injectableTypes;
        }

        public IEnumerable<T> GetInjectedInstances()
        {
            Type[] injectableTypes = GetInjectableTypes();
            return GetInjectedInstances(injectableTypes);
        }

        private IEnumerable<T> GetInjectedInstances(Type[] injectableTypes)
        {
            foreach(Type type in injectableTypes)
            {
                T instance = default;
                bool success = false;
                
                try
                {
                    instance = (T)Activator.CreateInstance(type);
                    success = true;
                }
                catch (MissingMethodException)
                {
                    Debug.LogError($"Could not load {typeof(T)} {type}");
                }
                catch (Exception e) 
                {
                    Debug.LogException(e);
                }

                if (success)
                {
                    yield return instance;
                }
            }
        }
    }
}
