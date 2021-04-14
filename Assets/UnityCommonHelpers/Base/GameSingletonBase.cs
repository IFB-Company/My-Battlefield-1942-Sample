using System;
using UnityEngine;

namespace Common.Singletons
{
    public abstract class GameSingletonBase<T> : MonoBehaviour
    {
        protected abstract T GetInstance();
        
        public static T Instance { get; protected set; }

        protected virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = GetInstance();
            }
            else
            {
                throw new Exception($"Some instance of {nameof(T)} already loaded!");
            }
        }
    }
}
