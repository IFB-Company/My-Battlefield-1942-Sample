using System;
using UnityEngine;

namespace Common
{
    public class TransformPointMover : MonoBehaviour
    {
        [SerializeField] private bool _isEnabled = true;

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { this._isEnabled = value; }
        }
        
        [SerializeField] private bool _isLoop = true;
        [SerializeField] private float _moveSpeed = 4f;

        private Vector3[] _currentPointsCollection;
        private Vector3 _currentPoint;
        private int _currentMoveIndex = 0;
        
        public bool IsDone { get; private set; }

        public void SetPoints(Vector3[] points)
        {
            _currentPointsCollection = points;
            if (points.Length > 0)
            {
                _currentPoint = _currentPointsCollection[0];
            }
        }

        private void Update()
        {
            if (!_isEnabled)
                return;

            MoveLoop();
        }

        private void MoveLoop()
        {
            if(_currentPointsCollection == null || _currentPointsCollection.Length <= 0)
                return;

            transform.position = Vector3.MoveTowards(transform.position, _currentPoint, _moveSpeed * Time.deltaTime);

            if (transform.position == _currentPoint)
            {
                _currentMoveIndex++;
                if (_currentMoveIndex > _currentPointsCollection.Length - 1)
                {
                    if (_isLoop)
                    {
                        _currentMoveIndex = 0;
                        transform.position = _currentPointsCollection[_currentMoveIndex];
                    }
                    else
                    {
                        IsDone = true;
                        return;
                    }
                }

                _currentPoint = _currentPointsCollection[_currentMoveIndex];
            }

        }
    }
}
