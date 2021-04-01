using _Scripts.Player.Controls;

namespace _Scripts.Static
{
    public static class GameHelper
    {
        public static class AxisConstants
        {
            public const string HORIZONTAL = "Horizontal";
            public const string VERTICAL = "Vertical";
            public const string MOUSE_X = "Mouse X";
            public const string MOUSE_Y = "Mouse Y";
        }

        public static class ControlNames
        {
            public const string ROTATION_JOYSTICK = "RotationJoystick";
            public const string MOVEMENT_JOYSTICK = "MovementJoystick";
        }

        public static class NullObjects
        {
            public static NullControlProvider NullControlProvider { get; } = new NullControlProvider();
        }
    }
}
