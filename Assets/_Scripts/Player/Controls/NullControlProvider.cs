using _Scripts.Player.Controls.Base;

namespace _Scripts.Player.Controls
{
    public class NullControlProvider : IControlProvider
    {
        public float GetX => 0f;
        public float GetY => 0f;
    }
}