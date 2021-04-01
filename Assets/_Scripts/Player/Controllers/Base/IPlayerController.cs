using _Scripts.Player.Controls.Base;
using _Scripts.Units.Base;

namespace _Scripts.Player.Controllers.Base
{
    public interface IPlayerController
    {
        BattleUnitBase CurrentBattleUnit { get; }
        IPlayerControlProvider ControlProvider { get; }
    }
}
