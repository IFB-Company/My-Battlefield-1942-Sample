using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class TransformFloatObj : MonoBehaviour
    {
        [SerializeField] private float _floatingSpeed = 6f;
        public float FloatingSpeed
        {
            get { return _floatingSpeed; }
            set { this._floatingSpeed = value; }
        }
        
        [SerializeField] private float _flyUpspeed = 12f;

        public float FlySpeed
        {
            get { return _flyUpspeed; }
            set { this._flyUpspeed = value; }
        }
        
        [SerializeField] private Vector3 _floatingDirection = Vector3.up;

        public Vector3 FloatingDirection
        {
            get { return _floatingDirection; }
            set { this._floatingDirection = value; }
        }
        
        [SerializeField] private bool _isEnable = false;
        public bool IsEnable
        {
            get { return _isEnable; }
            set { this._isEnable = value; }
        }
        
        [SerializeField] private Vector3 _targetOffset = Vector3.up;
    
        public Vector3 TargetOffset
        {
            get { return _targetOffset; }
            set { this._targetOffset = value; }
        }
        private Vector3 _targetPos;

        private bool _isReachTargetOffset;

        public bool IsReachTargetOffset
        {
            get => _isReachTargetOffset;
            set => _isReachTargetOffset = value;
        }
        
        public bool IsDisableOnReach { get; set; }

        private void Awake()
        {
            _targetPos = transform.position + _targetOffset;
        }

        public void UpdateTargetPos()
        {
            _targetPos = transform.position + _targetOffset;
        }
        
        public Action<TransformFloatObj> OnDisabledCallBack { get; set; }

        private void Update()
        {
            if (!_isEnable)
                return;

            if (_isReachTargetOffset)
            {
                float timeSin = Mathf.Sin(Time.time);
                transform.Translate(_floatingDirection * timeSin * _floatingSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPos, _flyUpspeed * Time.deltaTime);
                if (transform.position == _targetPos)
                {
                    _isReachTargetOffset = true;
                    if (IsDisableOnReach)
                    {
                        _isEnable = false;
                        //Fall simulation
                        var rb = gameObject.AddComponent<Rigidbody>();
                        rb.angularDrag = 25f;
                        rb.drag *= 2f;
                        rb.AddForce(Vector3.down * 5500f);
                        OnDisabledCallBack?.Invoke(this);
                    }
                }
            }
            
        }
    }
}
