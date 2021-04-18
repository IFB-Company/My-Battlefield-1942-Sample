using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Location
{
    public class SceneGamePool : MonoBehaviour
    {
        public static SceneGamePool Instance { get; private set; }
        
        private Dictionary<string, Stack<GameObject>> _poolDict;
        
        private void Awake()
        {
            Instance = this;
            InitDict();
        }

        private void InitDict()
        {
            _poolDict = new Dictionary<string, Stack<GameObject>>();
        }

        public GameObject GetObjectFromPool(GameObject prefab)
        {
            GameObject instanceToReturn = default;
            if (_poolDict.TryGetValue(prefab.name, out Stack<GameObject> poolStack))
            {
                if (poolStack.Any())
                {
                    var freeObject = poolStack.Pop();
                    if (freeObject != null)
                    {
                        freeObject.SetActive(true);
                        return freeObject;   
                    }
                }
            }

            instanceToReturn = Instantiate(prefab);
            
            return instanceToReturn;
        }

        public void AddObjectInPool(string originName, GameObject gameObjectToPool)
        {
            if (gameObjectToPool.activeInHierarchy)
            {
                Debug.LogError($"Attempt to add ACTIVE==TRUE object in pool! {gameObjectToPool.name}!");
                return;
            }
            if (_poolDict.TryGetValue(originName, out Stack<GameObject> poolStack))
            {
                poolStack.Push(gameObjectToPool);
            }
            else
            {
                var newPoolStack = new Stack<GameObject>();
                _poolDict.Add(originName, newPoolStack);
                newPoolStack.Push(gameObjectToPool);
            }
            
        }
    }
}
