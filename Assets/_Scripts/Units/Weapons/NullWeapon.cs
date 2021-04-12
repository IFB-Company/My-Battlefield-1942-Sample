using System;

namespace _Scripts.Units.Weapons
{
    public class NullWeapon : WeaponBase
    {
        private void OnValidate()
        {
            if (_aimAnchor == null)
            {
                _aimAnchor = transform;
            }
        }

        private void Awake()
        {
            if (_aimAnchor == null)
            {
                _aimAnchor = transform;
            }
        }

        protected override bool OnFire()
        {
            return false;
        }
    }
}