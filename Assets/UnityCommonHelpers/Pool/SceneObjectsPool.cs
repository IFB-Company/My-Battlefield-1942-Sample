using System;
using System.Collections.Generic;
using System.Linq;
using Injection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UnityHelpers.Pool
{
    public class SceneObjectsPool: IInjectable
    {
        private Dictionary<Type, Stack<object>> _objectsPoolDict = new Dictionary<Type, Stack<object>>();
        
        public T GetObjectFromPool<T>(T prefab) where T : MonoBehaviour
        {
            var type = typeof(T);
            if (!_objectsPoolDict.ContainsKey(type))
            {
                _objectsPoolDict.Add(type, new Stack<object>());
            }
            

            if (!_objectsPoolDict[type].Any())
            {
                return Object.Instantiate(prefab);
            }

            var objFromPool = (T) _objectsPoolDict[type].Pop();
            objFromPool.gameObject.SetActive(true);
            return  objFromPool;
        }
        
        public void AddObjectToPool<T>(T obj) where T : MonoBehaviour
        {
            var type = typeof(T);
            if (!_objectsPoolDict.ContainsKey(type))
            {
                _objectsPoolDict.Add(type, new Stack<object>());
            }

            if (obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(false);
            }
            _objectsPoolDict[type].Push(obj);
        }
    }
}
