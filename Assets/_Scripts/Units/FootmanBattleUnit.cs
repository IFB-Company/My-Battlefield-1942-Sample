using System;
using _Scripts.Units.Weapons;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units
{
    public class FootmanBattleUnit : GroundBattleUnit
    {
        private float _xRotation = 0f;
        [SerializeField] private Transform _faceObj;
        [SerializeField] private Transform _weaponAnchor;
        [SerializeField] private Transform _aimAnchor;
        public event Action<WeaponBase> OnWeaponChanged;

        protected override void Awake()
        {
            base.Awake();
            
            Assert.IsNotNull(_faceObj, "_faceObj != null");
            Assert.IsNotNull(_weaponAnchor, "_weaponAnchor != null");
            Assert.IsNotNull(_aimAnchor, "_aimAnchor != null");
        }

        protected override void MovementLoop()
        {
            base.MovementLoop();

            if (_targetRotDir.HasValue)
            {
                _xRotation -= _targetRotDir.Value.y;
                float maxAngle = 90f;
                _xRotation = Mathf.Clamp(_xRotation, -maxAngle, maxAngle);
                _faceObj.localRotation = Quaternion.Euler(_xRotation, 0f,0f);
            }
        }

        public void SetWeapon(WeaponBase newWeaponPrefab)
        {
            Assert.IsNotNull(newWeaponPrefab, "newWeaponPrefab != null");

            var currentWeapon = _weaponBase;
            if (currentWeapon != null)
            {
                Destroy(currentWeapon.gameObject);
            }

            var newWeaponInstance = Instantiate(newWeaponPrefab, _weaponAnchor);
            _weaponBase = newWeaponInstance;
            _weaponBase.SetAimAnchor(_aimAnchor);
            OnWeaponChanged?.Invoke(_weaponBase);
        }
    }
}
