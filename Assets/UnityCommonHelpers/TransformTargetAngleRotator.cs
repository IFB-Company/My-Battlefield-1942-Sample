using System;
using UnityEngine;

namespace Common
{
    public class TransformTargetAngleRotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _targetAngle = Vector3.zero;
        public Vector3 TargetAngle
        {
            get { return _targetAngle; }
            set { this._targetAngle = value; }
        }

        [SerializeField] private float _speed = 6f;
        public float Speed
        {
            get { return _speed; }
            set { this._speed = value; }
        }

        private Vector3 _startEulers;

        private void Start()
        {
            _startEulers = transform.eulerAngles;
        }
        
        public void ResetRotation()
        {
            _targetAngle = _startEulers;
        }

        private void Update()
        {
            transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, _targetAngle, _speed * Time.deltaTime);
        }
    }
}
