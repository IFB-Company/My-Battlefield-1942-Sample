using _Scripts.Units.Base;
using _Scripts.Units.Weapons;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units
{
    [RequireComponent(typeof(Collider))]
    public class HitCollider : MonoBehaviour
    {
        [SerializeField] private HittableObject _hittableObject;

        private void OnValidate()
        {
            if (_hittableObject == null)
            {
                _hittableObject = GetComponentInParent<HittableObject>();
            }
        }

        private void Awake()
        {
            Assert.IsNotNull(_hittableObject, "_hittableObject != null");
        }

        public void Damage(BattleUnitBase sender, WeaponData weaponData)
        {
            if (_hittableObject != null)
            {
                _hittableObject.Damage(sender, weaponData);
            }
        }
    }
}
