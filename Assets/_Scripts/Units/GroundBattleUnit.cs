using System;
using _Scripts.Units.Base;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Units
{
    public class GroundBattleUnit : BattleUnitBase
    {
        [SerializeField] protected Rigidbody _rigidbody;
        public Rigidbody NavMeshAgent => _rigidbody;

        private Vector3? _targetMoveDir;
        private Vector3? _targetRotDir;

        public override void MoveAtFrame(Vector3 dir)
        {
            _targetMoveDir = dir;
        }

        public override void RotateAtFrame(Vector3 dir)
        {
            _targetRotDir = dir;
        }

        private void FixedUpdate()
        {
            if (_rigidbody == null)
                return;
            
            if (_targetMoveDir.HasValue)
            {
                var moveDirValue = _targetMoveDir.Value;
                var forwardMove = transform.forward * moveDirValue.z;
                var sideMove = transform.right * moveDirValue.x;
                
                _rigidbody.MovePosition(transform.position + (forwardMove + sideMove) * 
                    _unitProperties.MoveSpeed * Time.fixedDeltaTime);
            }

            if (_targetRotDir.HasValue)
            {
                _rigidbody.angularVelocity = Vector3.zero;
                transform.Rotate(_targetRotDir.Value * _unitProperties.RotationSpeed * Time.fixedDeltaTime);    
            }

        }
    }
}
