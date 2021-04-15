using _Scripts.Player.Controls.Base;
using _Scripts.Units;
using _Scripts.Units.Base;
using UnityEngine;

namespace _Scripts.Player.Controllers.Base
{
    public interface IPlayerController
    {
        FootmanBattleUnit FootmanBattleUnit { get; }
        BattleUnitBase CurrentBattleUnit { get; }
        IPlayerControlProvider ControlProvider { get; }

        void SetCurrentBattleUnit(BattleUnitBase battleUnit);
    }
}
