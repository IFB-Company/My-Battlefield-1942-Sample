using System.Collections.Generic;
using _Scripts.Units.Weapons;
using UnityEngine;

namespace _Scripts.SettingsData
{
    [CreateAssetMenu(fileName = "UnitProperties", menuName = "Battle1942/UnitProperties", order = 0)]
    public class UnitProperties : ScriptableObject
    {
        [SerializeField] private float _moveSpeed = 10f;
        public float MoveSpeed => _moveSpeed;

        [SerializeField] private float _rotationSpeed = 10f;
        public float RotationSpeed => _rotationSpeed;

        [SerializeField] private int _maxHp = 100;
        public int MaxHp => _maxHp;

        [SerializeField] private int _armor = 1;
        public int Armor => _armor;

        [SerializeField] private WeaponType[] _ignoreWeaponTypes;
        public IReadOnlyCollection<WeaponType> IgnoreWeaponTypes => _ignoreWeaponTypes;
    }
}