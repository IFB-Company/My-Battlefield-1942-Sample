using TMPro;
using UnityEngine;

namespace UnityCommonHelpers
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public abstract class TextMeshUIControllerBase : MonoBehaviour
    {
        protected TextMeshProUGUI _textMesh;

        protected virtual void Awake()
        {
            _textMesh = GetComponent<TextMeshProUGUI>();
        }

        public virtual void SetText(string text)
        {
            if (_textMesh != null)
            {
                _textMesh.text = text;
            }
        }
    }
}
