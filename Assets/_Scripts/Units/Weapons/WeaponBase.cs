using System;
using UnityEngine;

namespace _Scripts.Units.Weapons
{
    public abstract class WeaponBase : MonoBehaviour
    {
        [SerializeField] protected WeaponData _weaponData;
        public WeaponData WeaponData => _weaponData;

        [SerializeField] protected Transform _aimAnchor;
        public Transform AimAnchor => _aimAnchor;
        public event Action<WeaponBase> OnFireEvent; 
        
        public virtual void Fire()
        {
            if (OnFire())
            {
                OnFireEvent?.Invoke(this);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Is success?</returns>
        protected abstract bool OnFire();
    }
}
