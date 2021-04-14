using System;
using System.Collections;
using Common.Data;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

namespace Common.Singletons
{
    public class SceneSwitchingManager : GameSingletonBase<SceneSwitchingManager>
    {
        [SerializeField] private float _delayAfterLoad = 1f;
        public float DelayAfterLoad => _delayAfterLoad;
        [SerializeField] private float _delayBeforeLoad = 0.5f;
        public float DelayBeforeLoad => _delayBeforeLoad;

        private WaitForSecondsRealtime _waitingBefore;
        private WaitForSecondsRealtime _waitingAfter;
        
        protected override SceneSwitchingManager GetInstance() => this;
        private Coroutine _sceneSwitchingCoroutine;
        
        /// <summary>
        /// int_param = Scene index in build settings
        /// </summary>
        public event Action<string> OnSceneStartLoading;
        
        /// <summary>
        /// int_param = Scene index in build settings
        /// </summary>
        public event Action<string> OnSceneLoaded;

        protected override void Awake()
        {
            base.Awake();

            if (_delayBeforeLoad > 0f)
            {
                _waitingBefore = new WaitForSecondsRealtime(_delayBeforeLoad);
            }
            
            if (_delayAfterLoad > 0f)
            {
                _waitingAfter = new WaitForSecondsRealtime(_delayAfterLoad);
            }
        }

        public void LoadSceneBySceneContainer(SceneContainer sceneContainer)
        {
            Assert.IsNotNull(sceneContainer, "sceneContainer != null");
            if (sceneContainer != null)
            {
                LoadSceneByName(sceneContainer.SceneName);
            }
        }
        
        public void LoadSceneByName(string sceneName)
        {
            if (_sceneSwitchingCoroutine != null)
            {
                Debug.LogError("Some scene already in loading progress.");
                return;
            }

            _sceneSwitchingCoroutine = StartCoroutine(SceneSwitchingCoroutine(sceneName));
        }
        
        private IEnumerator SceneSwitchingCoroutine(string sceneName)
        {
            OnSceneStartLoading?.Invoke(sceneName);
            yield return _delayBeforeLoad;
            
            AsyncOperation loadingAsyncOp = SceneManager.LoadSceneAsync(sceneName);
            while (!loadingAsyncOp.isDone)
            {
                yield return null;
            }

            yield return _delayAfterLoad;
            _sceneSwitchingCoroutine = null;
            OnSceneLoaded?.Invoke(sceneName);
        }
        
    }
}
