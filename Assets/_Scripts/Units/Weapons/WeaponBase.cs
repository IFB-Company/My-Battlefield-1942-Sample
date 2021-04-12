using UnityEngine;

namespace _Scripts.Units.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected WeaponData _weaponData;
        public WeaponData WeaponData => _weaponData;
        
        public abstract void Fire();
    }
}
