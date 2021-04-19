using System;
using _Scripts.Location;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units.Weapons
{
    public class StandartWeapon : WeaponBase
    {
        [SerializeField] protected GameObject _projectilePrefab;
        
        protected float _fireTimer;
        protected bool _isCanFire;
        
        protected override bool OnFire()
        {
            if (!_isCanFire)
                return false;

            FireProcess();
            _isCanFire = false;
            return true;
        }

        protected virtual void FireProcess()
        {
            Assert.IsNotNull(_projectilePrefab, "_projectilePrefab != null");

            var scenePool = SceneGamePool.Instance;
            Assert.IsNotNull(scenePool, "scenePool != null");
            var bullet = scenePool.GetObjectFromPool(_projectilePrefab);

            if (bullet.TryGetComponent(out IBullet bulletComponent))
            {
                bulletComponent.Initialize(_ownerUnit, _weaponData);
            }
            
            bullet.transform.position = _aimAnchor.transform.position;
            bullet.transform.rotation = _aimAnchor.transform.rotation;
            
        }

        protected virtual void Update()
        {
            TimerLoop();
        }

        protected virtual void TimerLoop()
        {
            if (_isCanFire)
                return;
            
            _fireTimer += Time.deltaTime;
            if (_fireTimer >= _weaponData.WeaponDelay)
            {
                _isCanFire = true;
                _fireTimer = 0;
            }
        }
    }
}
