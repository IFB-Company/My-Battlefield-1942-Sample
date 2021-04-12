using _Scripts.CommonStructs;
using UnityEngine;

namespace _Scripts.Units.Weapons
{
    [System.Serializable]
    public struct WeaponData
    {
        [SerializeField] private RangeData _damageRange;
        public RangeData DamageRange => _damageRange;
        
        [SerializeField] private float _weaponDelay;
        public float WeaponDelay => _weaponDelay;
        
        [SerializeField] private WeaponType _weaponType;
        public WeaponType WeaponType => _weaponType;

        [SerializeField] private bool _isInfiniteAmmunition;
        public bool IsInfiniteAmmunition => _isInfiniteAmmunition;

    }
}