using _Scripts.Player.Controls.Base;
using _Scripts.Units;
using _Scripts.Units.Base;

namespace _Scripts.Player.Controllers.Base
{
    public interface IPlayerController
    {
        FootmanBattleUnit FootmanBattleUnit { get; }
        BattleUnitBase CurrentBattleUnit { get; }
        IPlayerControlProvider ControlProvider { get; }
    }
}
