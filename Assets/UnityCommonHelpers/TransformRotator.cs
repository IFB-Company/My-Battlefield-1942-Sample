using System;
using UnityEngine;

namespace Common
{
    public class TransformRotator : MonoBehaviour
    {
        [SerializeField] private Vector3 _direction = Vector3.up;

        public Vector3 Direction
        {
            get => _direction;
            set => _direction = value;
        }

        [SerializeField] private float _rotSpeed = 8f;

        public float RotSpeed
        {
            get => _rotSpeed;
            set => _rotSpeed = value;
        }

        [SerializeField] private bool _isEnabled = true;

        public bool IsEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        private void Update()
        {
            if (!_isEnabled)
                return;
            transform.Rotate(_direction * _rotSpeed * Time.deltaTime);
        }
    }
}
