using UnityEngine;

namespace Common.Visual
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRendererColorChanger : ColorChangerBase
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        public override Color GetColor()
        {
            return _spriteRenderer.color;
        }
    }
}
