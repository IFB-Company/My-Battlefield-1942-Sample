﻿using _Scripts.Player.Controls.Enums;
using _Scripts.Units.Base;
using UnityEngine;

namespace _Scripts.Units
{
    public class GroundBattleUnit : BattleUnitBase
    {
        [SerializeField] protected Vector3 _jumpDir = Vector3.up;
        [SerializeField] protected float _jumpStrength = 500f;
        [SerializeField] protected Rigidbody _rigidbody;
        public Rigidbody Rigidbody => _rigidbody;

        [SerializeField] protected GroundChecker _groundChecker;

        protected Vector3? _targetMoveDir;
        protected Vector3? _targetRotDir;

        public override void MoveAtFrame(Vector3 dir)
        {
            _targetMoveDir = dir;
        }

        public override void RotateAtFrame(Vector3 dir)
        {
            _targetRotDir = dir;
        }

        protected virtual void FixedUpdate()
        {
            MovementLoop();
        }

        protected virtual void MovementLoop()
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
                transform.Rotate(Vector3.up * _targetRotDir.Value.x * _unitProperties.RotationSpeed * Time.fixedDeltaTime);    
            }
        }

        public override void HandleControlByButtonType(ButtonType buttonType)
        {
            if (buttonType == ButtonType.FIRE)
            {
                Fire();
            }
            else if (buttonType == ButtonType.JUMP)
            {
                Jump();
            }
        }

        protected virtual void Fire()
        {
            _weaponBase.Fire();
        }

        protected virtual void Jump()
        {
            bool isCanJump = _groundChecker != null ? _groundChecker.IsGrounded() : true;
            if(isCanJump)
                _rigidbody.AddForce(_jumpDir * _jumpStrength);
        }
    }
}
