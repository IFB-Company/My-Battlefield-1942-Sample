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

        [SerializeField] private int _maxAmmunition;
        public int MaxAmmunition => _maxAmmunition;

        [SerializeField] private int _currentAmmunition;
        public int CurrentAmmunition => _currentAmmunition;

        public WeaponData(WeaponData oldData, int currentAmmunition)
        {
            _damageRange = oldData.DamageRange;
            _weaponDelay = oldData.WeaponDelay;
            _weaponType = oldData.WeaponType;
            _isInfiniteAmmunition = oldData.IsInfiniteAmmunition;
            _maxAmmunition = oldData.MaxAmmunition;
            _currentAmmunition = currentAmmunition;
        }

    }
}