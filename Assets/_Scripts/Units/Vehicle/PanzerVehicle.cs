using _Scripts.Player.Controls.Enums;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units.Vehicle
{
    public class PanzerVehicle : BattleVehicleBase
    {
        [SerializeField] private float _moveTorque = 200f;
        [SerializeField] private float _rotTorque = 200f;
        [SerializeField] private float _rotTowerSpeed = 40f;
        [SerializeField] private float _rotMuzzleSpeed = 40f;
        [SerializeField] private float _muzzleMaxAngleX = 30f;
        
        [SerializeField] private Rigidbody _leadRb;
        [SerializeField] private ConstantForce _constantForce;
        
        [SerializeField] private float _clampSpeed = 5f;

        [SerializeField] private Transform _muzzle;
        [SerializeField] private Transform _tower;

        private float _muzzleXRot;

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
            
            Assert.IsNotNull(_muzzle, "_muzzle != null");
            Assert.IsNotNull(_tower, "_tower != null");
        }

        public override void MoveAtFrame(Vector3 dir)
        {
            _constantForce.force = transform.forward * dir.z * _moveTorque;
            transform.Rotate(Vector3.up * dir.x * _rotTorque * Time.deltaTime);
            _moveDir = dir;
        }

        public override void RotateAtFrame(Vector3 dir)
        {
            _tower.Rotate(Vector3.up * dir.x * _rotTowerSpeed * Time.deltaTime);

            _muzzleXRot -= (dir.y * _rotMuzzleSpeed * Time.deltaTime);
            var clampedXRot = Mathf.Clamp(_muzzleXRot, -_muzzleMaxAngleX, _muzzleMaxAngleX);
            _muzzle.localRotation = Quaternion.Euler(clampedXRot, 0, 0);
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

        public override void HandleControlByButtonType(ButtonType buttonType)
        {
            base.HandleControlByButtonType(buttonType);

            if (buttonType == ButtonType.FIRE)
            {
                if (_weaponBase != null)
                {
                    _weaponBase.Fire();
                }
            }
        }
    }
}
