using _Scripts.Player.Controls.Base;
using UnityEngine.Assertions;

namespace _Scripts.Player.Controls
{
    public class JoystickControlProvider : IControlProvider
    {
        private Joystick _joystick;

        public JoystickControlProvider(Joystick joystick)
        {
            Assert.IsNotNull(joystick, "joystick != null");
            _joystick = joystick;
        }

        public float GetX => _joystick.Horizontal;
        public float GetY => _joystick.Vertical;
    }
}
