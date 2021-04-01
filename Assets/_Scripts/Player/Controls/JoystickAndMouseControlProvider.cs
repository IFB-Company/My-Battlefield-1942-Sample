using _Scripts.Player.Controls.Base;
using _Scripts.Static;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Player.Controls
{
    public class JoystickAndMouseControlProvider : IControlProvider
    {
        private Joystick _joystick;
        public JoystickAndMouseControlProvider(Joystick joystick)
        {
            Assert.IsNotNull(joystick, "joystick != null");

            _joystick = joystick;
        }

        public float GetX
        {
            get
            {
                float x = _joystick.Horizontal + Input.GetAxis(GameHelper.AxisConstants.MOUSE_X);
                return x;
            }
        }

        public float GetY
        {
            get
            {
                float y = _joystick.Vertical + Input.GetAxis(GameHelper.AxisConstants.MOUSE_Y);
                return y;
            }
        }
    }
}