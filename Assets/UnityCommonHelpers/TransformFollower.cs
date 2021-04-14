using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class TransformFollower : MonoBehaviour
    {
        [SerializeField] protected Vector3 _offset;
        
        [SerializeField] protected Transform _followTransform;

        public Transform FollowTransform
        {
            get { return _followTransform; }
            set { this._followTransform = value; }
        }
        
        [SerializeField] protected float _moveSpeed = 6f;
        [SerializeField] protected float _rotSpeed = 6f;

        public virtual void SetFollowTransform(Transform target)
        {
            _followTransform = target;
        }
        
        protected virtual void Update()
        {
            if (_followTransform == null)
                return;

            var posToFollow = _followTransform.position + _offset;
            
            transform.position =
                Vector3.Lerp(transform.position, posToFollow, _moveSpeed * Time.deltaTime);
            transform.rotation =
                Quaternion.Lerp(transform.rotation, _followTransform.rotation, _rotSpeed * Time.deltaTime);
        }
    }
}
