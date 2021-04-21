using UnityEngine;

namespace _Scripts.Units
{
    public class VehicleCoalDisappearBehaviour : DisappearBehaviour
    {
        [SerializeField] private MeshRenderer[] _meshRenderers;

        private bool _isAlreadyCoaled;
        private static readonly int Color1 = Shader.PropertyToID("_Color");

        protected override void OnValidate()
        {
            base.OnValidate();

            _meshRenderers = GetComponentsInChildren<MeshRenderer>();
        }

        protected override void OnEnable()
        {
            if (!_isAlreadyCoaled)
            {
                PaintAsCoal();
                _isAlreadyCoaled = true;
            }
            
            base.OnEnable();
        }

        private void PaintAsCoal()
        {
            foreach (var meshRend in _meshRenderers)
            {
                if (meshRend != null)
                {
                    var mat = meshRend.material;
                    if (mat != null)
                    {
                        mat.SetColor(Color1, Color.black);
                    }
                }
            }
        }
    }
}
