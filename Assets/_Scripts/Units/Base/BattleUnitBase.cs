using _Scripts.SettingsData;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units.Base
{
    public abstract class BattleUnitBase : MonoBehaviour
    {
        [SerializeField] protected UnitProperties _unitProperties;

        protected virtual void Awake()
        {
            Assert.IsNotNull(_unitProperties, "_unitProperties != null");
        }
        
        public abstract void MoveAtFrame(Vector3 dir);
        public abstract void RotateAtFrame(Vector3 dir);
    }
}
