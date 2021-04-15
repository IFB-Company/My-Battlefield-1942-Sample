using _Scripts.Player.Controls.Enums;
using _Scripts.SettingsData;
using _Scripts.Units.Weapons;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units.Base
{
    public abstract class BattleUnitBase : MonoBehaviour
    {
        [SerializeField] protected Transform _cameraPosition;
        public Transform CameraPosition => _cameraPosition;
        
        [SerializeField] protected UnitProperties _unitProperties;
        [SerializeField] protected WeaponBase _weaponBase;

        private void OnValidate()
        {
            if (_weaponBase == null)
            {
                _weaponBase = FindObjectOfType<NullWeapon>();
            }
        }

        protected virtual void Awake()
        {
            Assert.IsNotNull(_unitProperties, "_unitProperties != null");
            
            if (_weaponBase == null)
            {
                _weaponBase = FindObjectOfType<NullWeapon>();
            }
            
            Assert.IsNotNull(_weaponBase, "_weaponBase != null");
            
        }
        
        public abstract void MoveAtFrame(Vector3 dir);
        public abstract void RotateAtFrame(Vector3 dir);

        public virtual void HandleControlByButtonType(ButtonType buttonType)
        {
            
        }

        public void Damage(int damage, BattleUnitBase damagerUnit)
        {
            Debug.Log($"{name} get damage {damage} from {damagerUnit.name}!");
        }
    }
}
