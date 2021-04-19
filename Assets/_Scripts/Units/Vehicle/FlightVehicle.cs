﻿using _Scripts.Player.Controls.Enums;
using _Scripts.Units.Weapons;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units.Vehicle
{
    public class FlightVehicle : BattleVehicleBase
    {
        [SerializeField] private Rigidbody _manRb;
        [SerializeField] private float _maxSpeed = 5f;

        [Space]
        [SerializeField] private Vector3 _propellerMoveDir = new Vector3(0, 1, 0);
        [SerializeField] private Transform _propeller;
        [SerializeField] private float _propellerSpeed = 8f;
        [SerializeField] private float _upForce = 100f;
        [SerializeField] private float _agilityForce = 10000f;
        [SerializeField] private float _maxAltitude = 100f;
        [SerializeField] private Transform _body;
        [SerializeField] private float _flightTime = 3f;
        [SerializeField] private Vector3 _targetBodyFlightEulers;

        [Space]
        [SerializeField] private WeaponBase[] _additionalWeapons;
        [SerializeField] private WeaponBase[] _bombSpawnWeapons;

        private float _flightTimer;
        private bool _isFlightUpDone;
        private float _currentPropellerSpeed;
        private float _propellerSpeedPerFrame;
        
        

        protected override void Awake()
        {
            base.Awake();
            Assert.IsNotNull(_manRb, "_manRb != null");
            Assert.IsNotNull(_body, "_body != null");
        }
        
        public override void MoveAtFrame(Vector3 dir)
        {

            if (dir.z > Mathf.Epsilon)
            {
                _manRb.AddForce(transform.forward * dir.z * _unitProperties.MoveSpeed * Time.deltaTime);    
            }
            

            transform.Rotate(Vector3.up * dir.x * _unitProperties.RotationSpeed * Time.deltaTime);


            float maxPropellerValue = _propellerSpeed * Time.deltaTime;
            _currentPropellerSpeed += dir.z * _propellerSpeed * Time.deltaTime;
            _currentPropellerSpeed = Mathf.Clamp(_currentPropellerSpeed, -maxPropellerValue, maxPropellerValue);
            _propellerSpeedPerFrame = Mathf.Lerp(_propellerSpeedPerFrame, _currentPropellerSpeed,
                _propellerSpeed * Time.deltaTime);
            
            _manRb.useGravity = Mathf.Approximately(dir.z, 0f);

        }

        public override void RotateAtFrame(Vector3 dir)
        {
            if (!_isFlightUpDone)
                return;
            
            var rotVector = new Vector3(
                dir.y,
                0,
                -dir.x
            );

            transform.Rotate(rotVector * _agilityForce * Time.deltaTime);
        }

        private void Update()
        {
            if (!IsOnControl)
                return;
            
            CheckFlightLoop();
            PropellerLoop();
        }

        private void PropellerLoop()
        {
            if (_propeller != null)
            {
                _propeller.Rotate(_propellerMoveDir * _propellerSpeedPerFrame);
            }
        }

        private void CheckFlightLoop()
        {
            if (_isFlightUpDone)
                return;

            if (_manRb.velocity.z <= Mathf.Epsilon)
                return;
            
            if (!Mathf.Approximately(_manRb.velocity.z, 0f))
            {
                if (_flightTimer >= _flightTime)
                {
                    _flightTimer = 0;
                    _isFlightUpDone = true;
                    OnFlightUpDone();
                }
                _flightTimer += Time.deltaTime;
            }
            else
            {
                _flightTimer = 0;
            }
            
        }

        private void OnFlightUpDone()
        {
            _body.DOLocalRotate(_targetBodyFlightEulers, 1f);
            _manRb.AddForce(Vector3.up * _upForce);
        }

        private void FixedUpdate()
        {
            ClampSpeedLoop();
        }
        

        private void ClampSpeedLoop()
        {
            _manRb.velocity = Vector3.ClampMagnitude(_manRb.velocity, _maxSpeed);
            _manRb.angularVelocity = Vector3.zero;
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

                foreach (var additionalWeapon in _additionalWeapons)
                {
                    if (additionalWeapon != null)
                    {
                        additionalWeapon.Fire();
                    }
                }
            }
            else if (buttonType == ButtonType.BOMB_DROP)
            {
                foreach (var bombSpawnWeapon in _bombSpawnWeapons)
                {
                    if (bombSpawnWeapon != null)
                    {
                        bombSpawnWeapon.Fire();
                    }
                }
            }
        }
    }
}
