using _Scripts.Player.Controls.Base;
using _Scripts.Static;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Player.Controls
{
    public class JoystickAndKeyboardControlProvider : IControlProvider
    {
        private Joystick _joystick;
        
        public JoystickAndKeyboardControlProvider(Joystick joystick)
        {
            Assert.IsNotNull(joystick, "joystick != null");

            _joystick = joystick;
        }

        public float GetX => _joystick.Horizontal + Input.GetAxis(GameHelper.AxisConstants.HORIZONTAL);
        public float GetY => _joystick.Vertical + Input.GetAxis(GameHelper.AxisConstants.VERTICAL);
    }
}