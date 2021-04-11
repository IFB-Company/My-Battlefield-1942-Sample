using System;
using _Scripts.Player.Controls.Enums;

namespace _Scripts.Player.Controls.Base
{
    public interface IButtonControlProvider
    {
        event Action<ButtonType> OnButtonPressedEvent;
    }
}
