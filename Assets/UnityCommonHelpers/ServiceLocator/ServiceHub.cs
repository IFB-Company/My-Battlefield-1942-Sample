using System;
using UnityEngine;

namespace Common.ServiceLocator
{
    public class ServiceHub : MonoBehaviour
    {
        [SerializeField] protected GameObject[] _servicePrefabs;
        public event Action<IGameService> OnGameServiceCreated;

        public event Action OnServicesLoaded;

        protected virtual void Start()
        {
            InitStandartServices();
            InitMonoBehaviorServices();
            
            DontDestroyOnLoad(this.gameObject);
            
            OnServicesLoaded?.Invoke();
        }

        /// <summary>
        /// Default services not inherited  by MonoBehaviour 
        /// </summary>
        protected virtual void InitStandartServices()
        {
            
        }

        protected virtual void InitMonoBehaviorServices()
        {
            for (int i = 0; i < _servicePrefabs.Length; i++)
            {
                var prefab = _servicePrefabs[i];
                if (prefab == null)
                {
                    Debug.LogError("Some service prefab is missing!");
                }
                else
                {
                    var instance = Instantiate(prefab, transform);
                    var serviceComponent = instance.GetComponent<IGameService>();
                    OnGameServiceCreated?.Invoke(serviceComponent);
                }
            }
        }
    }  
}

