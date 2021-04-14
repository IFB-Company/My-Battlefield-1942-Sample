using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Common.Data
{
    [CreateAssetMenu(fileName = "SceneContainer", menuName = "CommonData/SceneContainer")]
    public class SceneContainer : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] protected SceneAsset _sceneAsset;

        protected virtual void OnValidate()
        {
            _sceneName = _sceneAsset != null ? _sceneAsset.name : "UNKNOWN";
        }
#endif
        [SerializeField] protected string _sceneName;
        public string SceneName => _sceneName;
    }
}