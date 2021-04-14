using Common.Singletons;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Common.Loading
{
    public class LoadingVisualController : MonoBehaviour
    {
        [SerializeField] private SceneSwitchingManager _switchingManager;
            
        [SerializeField] private UnityEvent _onStartLoad;
        [SerializeField] private UnityEvent _onStopLoad;

        private void Awake()
        {
            Assert.IsNotNull(_switchingManager, "_switchingManager != null");
            if (_switchingManager != null)
            {
                _switchingManager.OnSceneLoaded += OnSceneStartLoad;
                _switchingManager.OnSceneLoaded += OnSceneLoaded;
            }
        }

        private void OnDestroy()
        {
            if (_switchingManager != null)
            {
                _switchingManager.OnSceneStartLoading -= OnSceneStartLoad;
                _switchingManager.OnSceneLoaded -= OnSceneLoaded;
            }
        }

        private void OnSceneLoaded(string sceneName)
        {
            _onStopLoad?.Invoke();
        }

        private void OnSceneStartLoad(string sceneName)
        {
            _onStartLoad?.Invoke();
        }
    }
}

