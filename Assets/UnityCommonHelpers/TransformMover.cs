using UnityEngine;

namespace Common 
{
    public class TransformMover : MonoBehaviour
    {
        [SerializeField] private bool _isEnabled = true;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { this._isEnabled = value; }
        }
        
        [SerializeField] private Vector3 _direction = Vector3.forward;
        public Vector3 Direction
        {
            get { return _direction; }
            set { this._direction = value; }
        }
        
        [SerializeField] private float _speed = 5f;
        public float Speed
        {
            get { return _speed; }
            set { this._speed = value; }
        }
        
        void Update()
        {
            if (!_isEnabled)
                return;
            transform.Translate(_direction * _speed * Time.deltaTime);
        }
    } 
}