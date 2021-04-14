using System;
using Common.Data;
using Common.Singletons;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonSceneSwitcher : MonoBehaviour
    {
        [SerializeField] private SceneContainer _sceneContainer;
        [SerializeField] private string _sceneName;
        private void Awake()
        {
            if (string.IsNullOrEmpty(_sceneName) && _sceneContainer == null)
            {
                Debug.LogError($"SceneName is missing!");
                return;
            }
            var btn = this.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(OnClick);
            }
            else
            {
                Debug.LogError("Button is missing!");
            }
        }

        private void OnValidate()
        {
            if (_sceneContainer != null)
            {
                _sceneName = _sceneContainer.SceneName;
            }
        }

        private void OnClick()
        {
            if (SceneSwitchingManager.Instance == null)
            {
                Debug.LogError($"{nameof(SceneSwitchingManager)} is missing!");
                return;
            }

            if (string.IsNullOrEmpty(_sceneName) && _sceneContainer != null)
            {
                _sceneName = _sceneContainer.SceneName;
            }
            
            SceneSwitchingManager.Instance.LoadSceneByName(_sceneName);
        }
    }
}

