using System;
using UnityEngine;

namespace Common
{
    public class TransformScaler : MonoBehaviour
    {
        [SerializeField] private bool _isEnabled = true;
        public bool IsEnabled
        {
            get => _isEnabled;
            set => _isEnabled = value;
        }

        [SerializeField] private bool _isLerp = true;

        [SerializeField] private float _speed = 6f;
        [SerializeField] private Vector3 _currentScale = Vector3.one;

        public Vector3 CurrentScale
        {
            get { return _currentScale; }
            set { _currentScale = value; }
        }
        
        [SerializeField] private Vector3 _showingScale = Vector3.one;
        [SerializeField] private Vector3 _hiddenScale = Vector3.zero;
        

        public void Show()
        {
            _currentScale = _showingScale;
        }

        public void Hide()
        {
            _currentScale = _hiddenScale;
        }
        
        private void Update()
        {
            if (!_isEnabled)
                return;

            if (_isLerp)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, _currentScale, _speed * Time.deltaTime);
            }
            else
            {
                transform.localScale = Vector3.MoveTowards(transform.localScale, _currentScale, _speed * Time.deltaTime);
            }
            
        }
    }
}
