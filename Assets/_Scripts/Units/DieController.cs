using System;
using _Scripts.Location;
using _Scripts.Units.Base;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace _Scripts.Units
{
    public class DieController : MonoBehaviour
    {
        [SerializeField] private BattleUnitBase _unit;
        [SerializeField] private HittableObject _hittableObject;
        [SerializeField] private UnityEvent _onDieUnityEvent;
        [SerializeField] private GameObject _dieEffectPrefab;
        [SerializeField] private GameObject _dieModelPrefab;
        [SerializeField] private Vector2 _dieAffectRange = new Vector2(100, 1500);
        [SerializeField] private Vector3 _dieAffectDirection = new Vector3(0, 1, 0);


        public event Action<HittableObject> OnDie; 
        
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
            ReleaseCamera();
            ShowEffect();
            ShowDieModel();
            
            _onDieUnityEvent?.Invoke();
            OnDie?.Invoke(_hittableObject);
        }

        private void ReleaseCamera()
        {
            if (_unit != null)
            {
                _unit.CameraPosition.parent = null;
            }
        }

        private void ShowDieModel()
        {
            if (_dieModelPrefab != null)
            {
                var sceneGamePool = SceneGamePool.Instance;
                GameObject effectInstance = null;
                if (sceneGamePool != null)
                {
                    effectInstance = sceneGamePool.GetObjectFromPool(_dieModelPrefab);
                }
                else
                {
                    effectInstance = Instantiate(_dieModelPrefab);
                }

                effectInstance.transform.position = transform.position;
                effectInstance.transform.rotation = transform.rotation;

                var rb = effectInstance.GetComponentInChildren<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(_dieAffectDirection * Random.Range(_dieAffectRange.x, _dieAffectRange.y));
                }
                
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
