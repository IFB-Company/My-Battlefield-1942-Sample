using System;
using _Scripts.Location;
using UnityEngine;

namespace _Scripts.Units
{
    public class OnDisableEffectSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;
        private void OnDisable()
        {
            if (_effectPrefab != null)
            {
                var sceneGamePool = SceneGamePool.Instance;
                GameObject effectInstance = null;
                if (sceneGamePool != null)
                {
                    effectInstance = sceneGamePool.GetObjectFromPool(_effectPrefab);
                }
                else
                {
                    effectInstance = Instantiate(_effectPrefab);
                }

                effectInstance.transform.position = transform.position;
                effectInstance.transform.rotation = transform.rotation;
            }
        }
    }
}
