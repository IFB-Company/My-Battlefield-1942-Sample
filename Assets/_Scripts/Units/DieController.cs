using System;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace _Scripts.Units
{
    public class DieController : MonoBehaviour
    {
        [SerializeField] private HittableObject _hittableObject;
        [SerializeField] private UnityEvent _onDieUnityEvent;
        
        private void Awake()
        {
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
            _onDieUnityEvent?.Invoke();
        }
    }
}
