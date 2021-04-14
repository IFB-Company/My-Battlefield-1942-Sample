using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Singletons
{
    public class GamePreloader : GameSingletonBase<GamePreloader>
    {
        [SerializeField] private GameObject[] _managersToPreload;
        [SerializeField] private float _delayBetweenLoad = 0.5f;
        [SerializeField] private SceneSwitchingManager _switchingManager;
        [SerializeField] private string _nextSceneName;
        [SerializeField] private bool _isDontDestroyComponentAtManagers = true;
        
        // Make Preloader as SingletonBase to avoid duplicate loadings and throw expection
        protected override GamePreloader GetInstance() => this;

        protected override void Awake()
        {
            base.Awake(); // < avoid duplicate loadings and throw expection
            
            Assert.IsNotNull(_switchingManager, "_switchingManager != null");
        }

        private void Start()
        {
            StartCoroutine(PreloadProcessCoroutine());
        }

        private IEnumerator PreloadProcessCoroutine()
        {
            var waiting = new WaitForSeconds(_delayBetweenLoad);
            for (int i = 0; i < _managersToPreload.Length; i++)
            {
                yield return waiting;
                var prefab = _managersToPreload[i];
                if (prefab != null)
                {
                    var managerGO = Instantiate(prefab);

                    if (_isDontDestroyComponentAtManagers)
                        managerGO.AddComponent<DoLComponent>();
                }
                else
                {
                    Debug.LogError("Some prefab in array is NULL!!");
                }
            }

            yield return waiting;
            
            if(_switchingManager != null)
                _switchingManager.LoadSceneByName(_nextSceneName);

        }
    }
}
