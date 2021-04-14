using UnityEngine;

namespace Common.Visual
{
    public abstract class ColorChangerBase : MonoBehaviour
    {
        public abstract void SetColor(Color color);
        public abstract Color GetColor();
    }
}

