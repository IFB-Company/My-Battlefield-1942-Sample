using System;
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
            _onDieUnityEvent?.Invoke();
        }

        private void ReleaseCamera()
        {
            if (_unit != null)
            {
                _unit.CameraPosition.parent = null;
            }
        }
    }
}
