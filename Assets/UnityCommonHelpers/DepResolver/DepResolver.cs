using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Dependencies
{
    public static class DepResolver
    {
        private static Dictionary<Type, ISingletonDependency> _depDict;

        public static void RegisterDependency<T>(T instance) where T : ISingletonDependency
        {
            if (_depDict == null)
            {
                _depDict = new Dictionary<Type, ISingletonDependency>();
            }

            var type = typeof(T);
            if (_depDict.ContainsKey(type))
            {
                Debug.LogError($"Singleton with type {type.FullName} already declared!");
            }
            else
            {
                _depDict[type] = instance;
            }
        }

        public static T Resolve<T>() where T : ISingletonDependency
        {
            var type = typeof(T);
            if (_depDict.ContainsKey(type))
            {
                return (T) _depDict[type];
            }
            Debug.LogError($"There is no instance for type {type.FullName}!");
            return default(T);
        }
    }
}

