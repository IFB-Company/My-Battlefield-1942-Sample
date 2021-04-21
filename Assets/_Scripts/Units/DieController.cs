using System;
using _Scripts.Location;
using _Scripts.Units.Base;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace _Scripts.Units
{
    public class DieController : MonoBehaviour
    {
        [SerializeField] private BattleUnitBase _unit;
        [SerializeField] private HittableObject _hittableObject;
        [SerializeField] private UnityEvent _onDieUnityEvent;
        [SerializeField] private GameObject _dieEffectPrefab;

        private void OnValidate()
        {
            _unit = GetComponent<BattleUnitBase>();
        }

        private void Awake()
        {
            Assert.IsNotNull(_unit, "_unit != null");
            Assert.IsNotNull(_hittableObject, "_hittableObject != null");
            _hittableObject.OnDie += HittableObjectOnDie;
        }

        private void OnDestroy()
        {
            _hittableObject.OnDie -= HittableObjectOnDie;
        }

        private void HittableObjectOnDie()
        {
            //TODO: Realize real die
            ReleaseCamera();
            ShowEffect();
            _onDieUnityEvent?.Invoke();
        }

        private void ReleaseCamera()
        {
            if (_unit != null)
            {
                _unit.CameraPosition.parent = null;
            }
        }

        private void ShowEffect()
        {
            if (_dieEffectPrefab != null)
            {
                var sceneGamePool = SceneGamePool.Instance;
                GameObject effectInstance = null;
                if (sceneGamePool != null)
                {
                    effectInstance = sceneGamePool.GetObjectFromPool(_dieEffectPrefab);
                }
                else
                {
                    effectInstance = Instantiate(_dieEffectPrefab);
                }

                effectInstance.transform.position = transform.position;
                effectInstance.transform.rotation = transform.rotation;
            }
        }
    }
}
