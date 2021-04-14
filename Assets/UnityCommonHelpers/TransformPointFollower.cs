using System;
using UnityEngine;

namespace Common
{
    public class TransformPointFollower : MonoBehaviour
    {
        [SerializeField] private Vector3 _targetPoint;
        public Vector3 TargetPoint
        {
            get { return _targetPoint; }
            set { this._targetPoint = value; }
        }

        [SerializeField] private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { this._isEnabled = value; }
        }

        [SerializeField] private float _moveSpeed = 6f;
        public float MoveSpeed
        {
            get { return _moveSpeed; }
            set { this._moveSpeed = value; }
        }

        private void Update()
        {
            if (!_isEnabled)
                return;
            transform.position = Vector3.Lerp(transform.position, _targetPoint, _moveSpeed * Time.deltaTime);
        }
    }
}
