using Injection;
using UnityEngine;

namespace UnityHelpers.Injection
{
    public abstract class PrefabHolder<T> : IInjectable where T : Object
    {
        public T Prefab { get; protected set; }
        
        public PrefabHolder(T prefabLink)
        {
            Prefab = prefabLink;
        }
    }
}