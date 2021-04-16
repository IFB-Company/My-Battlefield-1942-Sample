using System;
using _Scripts.Units.Base;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units.Weapons
{
    public class ProjectileBullet : MonoBehaviour, IBullet
    {
        [SerializeField] private float _projectileLength = 0.2f;
        
        private bool _isInitialized;
        private WeaponData _weaponData;
        private BattleUnitBase _owner;
        
        public void Initialize(BattleUnitBase source, WeaponData weaponData)
        {
            _weaponData = weaponData;
            _owner = source;
            _isInitialized = true;
        }

        private void OnDisable()
        {
            _isInitialized = false;
        }

        private void Update()
        {
            if (!_isInitialized)
                return;
            
            ProjectileLoop();
        }

        private void ProjectileLoop()
        {
            #if UNITY_EDITOR
            Debug.DrawLine(transform.position, transform.position + transform.forward, Color.red, 0.5f);
            #endif

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _projectileLength))
            {
                if (hit.collider.TryGetComponent(out HitCollider hitCollider))
                {
                    hitCollider.Damage(_owner, _weaponData);
                    Debug.Log($"Damage: {hitCollider.name}");
                }
                
                gameObject.SetActive(false);
            }
        }
    }
}
