using System;
using System.Linq;
using _Scripts.SettingsData;
using _Scripts.Units.Base;
using _Scripts.Units.Data;
using _Scripts.Units.Weapons;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units
{
    public class HittableObject : MonoBehaviour
    {
        [SerializeField] private UnitProperties _unitProperties;
        public UnitProperties UnitProperties => _unitProperties;
        
        [Space]
        [Header("Runtime")]
        [SerializeField] private HittableData _currentHittableData;

        public HittableData CurrentHittableData => _currentHittableData;
        
        /// <summary>
        /// int - damage that dealt
        /// </summary>
        public event Action<int> OnDamaged;
        public event Action OnDie;

        private void Awake()
        {
            Assert.IsNotNull(_unitProperties, "_unitProperties != null");

            _currentHittableData = 
                new HittableData(_unitProperties.MaxHp, false);
        }

        public void Kill(BattleUnitBase sender)
        {
            var damage = Mathf.RoundToInt(_unitProperties.MaxHp);
            damage = Mathf.Clamp(damage, 0, Mathf.Abs(damage));
            var lastHp = _currentHittableData.CurrentHp;
            var nextHp = lastHp - damage;
            nextHp = Mathf.Clamp(nextHp, 0, Mathf.Abs(nextHp));
            bool isDead = nextHp <= 0;
            _currentHittableData = new HittableData(nextHp, isDead);

            if (isDead)
            {
                OnDie?.Invoke();
            }
                
            OnDamaged?.Invoke(damage);
        }

        public void Damage(BattleUnitBase sender, WeaponData weaponData)
        {
            if (_currentHittableData.IsDead)
                return;
            
            if (!_unitProperties.IgnoreWeaponTypes.Contains(weaponData.WeaponType))
            {
                var damage = Mathf.RoundToInt(weaponData.DamageRange.GetValue - _unitProperties.Armor);
                damage = Mathf.Clamp(damage, 0, Mathf.Abs(damage));
                var lastHp = _currentHittableData.CurrentHp;
                var nextHp = lastHp - damage;
                nextHp = Mathf.Clamp(nextHp, 0, Mathf.Abs(nextHp));
                bool isDead = nextHp <= 0;
                _currentHittableData = new HittableData(nextHp, isDead);

                if (isDead)
                {
                    OnDie?.Invoke();
                }
                
                OnDamaged?.Invoke(damage);
            }

        }
    }
}
