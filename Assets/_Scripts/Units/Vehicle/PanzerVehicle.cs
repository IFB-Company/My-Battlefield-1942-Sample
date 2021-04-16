﻿using System;
using UnityEngine;

namespace _Scripts.Units.Vehicle
{
    public class PanzerVehicle : BattleVehicleBase
    {
        [SerializeField] private float _moveTorque = 200f;
        [SerializeField] private float _rotTorque = 200f;
        
        [SerializeField] private Rigidbody _leadRb;
        [SerializeField] private ConstantForce _constantForce;
        
        [SerializeField] private float _clampSpeed = 5f;


        private Vector3? _moveDir;
        private Vector3? _rotDir;
        
        protected virtual void OnValidate()
        {
            _leadRb = GetComponent<Rigidbody>();
            _constantForce = GetComponent<ConstantForce>();
        }
        
        protected override void Awake()
        {
            base.Awake();
        }

        public override void MoveAtFrame(Vector3 dir)
        {
            _constantForce.force = transform.forward * dir.z * _moveTorque;
            transform.Rotate(Vector3.up * dir.x * _rotTorque * Time.deltaTime);
            _moveDir = dir;
        }

        public override void RotateAtFrame(Vector3 dir)
        {
            _rotDir = dir;
        }

        private void FixedUpdate()
        {
            PhysicsLoop();
        }

        private void PhysicsLoop()
        {
            if (_leadRb != null)
            {
                _leadRb.velocity = Vector3.ClampMagnitude(_leadRb.velocity, _clampSpeed);
            }

            if (_moveDir.HasValue)
            {
                //_leadRb.AddTorque(transform.up * _moveDir.Value.x * _moveTorque);
            }
        }
        
    }
}
