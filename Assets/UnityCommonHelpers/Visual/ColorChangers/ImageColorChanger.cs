using UnityEngine;
using UnityEngine.UI;

namespace Common.Visual
{
    [RequireComponent(typeof(Image))]
    public class ImageColorChanger : ColorChangerBase
    {
        private Image _img;

        private void Awake()
        {
            _img = GetComponent<Image>();
        }

        public override void SetColor(Color color)
        {
            _img.color = color;
        }

        public override Color GetColor()
        {
            if(_img == null) 
                return Color.black;
            return _img.color;
        }
    }
}

