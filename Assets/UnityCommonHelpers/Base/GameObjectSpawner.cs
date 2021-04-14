using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnityCommonHelpers.Base
{
    public abstract class GameObjectSpawner<T> : MonoBehaviour where T : MonoBehaviour
    {
        [SerializeField] protected Transform _spawnRoot;
        [SerializeField] protected T _prefab;
        [SerializeField] protected Vector3 _defaultSpawnPoint;
        [SerializeField] protected List<T> _preloadedObjects;
        
        protected LinkedList<T> _preloadedObjectsWorkList;
        
        public event Action<T> OnObjectSpawned;


        protected virtual void Start()
        {
            _preloadedObjectsWorkList = new LinkedList<T>(_preloadedObjects);
        }

        public T Spawn(Vector3 position)
        {
            T instance = null;
            if (_preloadedObjectsWorkList.Any())
            {
                instance = _preloadedObjectsWorkList.FirstOrDefault();
                instance.gameObject.SetActive(true);
                _preloadedObjectsWorkList.Remove(instance);

            }
            else
            {
                instance = Instantiate(_prefab, _spawnRoot);   
            }
            instance.transform.position = position;
            OnObjectSpawned?.Invoke(instance);
            return instance;
        }
    }
}
