using System;
using _Scripts.Units.Base;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units.Vehicle
{
    public abstract class BattleVehicleBase : BattleUnitBase
    {
        protected FootmanBattleUnit _pilot;
        public FootmanBattleUnit Pilot => _pilot;

        public bool IsOnControl  => _pilot != null;

        public event Action<BattleVehicleBase, FootmanBattleUnit> OnEnterInVehicle; 
        public event Action<BattleVehicleBase, FootmanBattleUnit> OnLeaveFromVehicle; 
        
        public virtual void EnterVehicle(FootmanBattleUnit footmanBattleUnit)
        {
            if (IsOnControl)
            {
                Debug.LogError($"Cannot Enter vehicle it is already on control!");
                return;
            }
            Assert.IsNotNull(footmanBattleUnit, "footmanBattleUnit != null");
            _pilot = footmanBattleUnit;
            
            OnEnterInVehicle?.Invoke(this, _pilot);
        }

        public virtual void LeaveVehicle()
        {
            if (!IsOnControl)
            {
                Debug.LogError($"LEave from vehicle that is'nt on control!");
                return;
            }
            
            OnLeaveFromVehicle?.Invoke(this, _pilot);
            _pilot = null;
        }
    }
}
