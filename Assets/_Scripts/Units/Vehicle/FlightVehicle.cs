using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units.Vehicle
{
    public class FlightVehicle : BattleVehicleBase
    {
        [SerializeField] private Rigidbody _manRb;
        [SerializeField] private float _maxSpeed = 5f;
        [SerializeField] private ConstantForce _constantForce;
        
        [Space]
        [SerializeField] private Vector3 _propellerMoveDir = new Vector3(0, 1, 0);
        [SerializeField] private Transform _propeller;
        [SerializeField] private float _propellerSpeed = 8f;

        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(_manRb, "_manRb != null");
            Assert.IsNotNull(_constantForce, "_constantForce != null");
        }

        public override void MoveAtFrame(Vector3 dir)
        {
            _constantForce.force = transform.forward * _unitProperties.MoveSpeed * dir.z;

            if (_propeller != null)
            {
                _propeller.Rotate(_propellerMoveDir * dir.z * _propellerSpeed * Time.deltaTime);
            }
        }

        public override void RotateAtFrame(Vector3 dir)
        {
            
        }

        private void FixedUpdate()
        {
            ClampSpeedLoop();
        }

        private void ClampSpeedLoop()
        {
            _manRb.velocity = Vector3.ClampMagnitude(_manRb.velocity, _maxSpeed);
        }
    }
}
