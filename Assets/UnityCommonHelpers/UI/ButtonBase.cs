using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Common.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonBase : MonoBehaviour
    {
        protected virtual void Awake()
        {
            Button btn = this.GetComponent<Button>();
            Assert.IsNotNull(btn, "btn != null");
            if (btn != null)
            {
                btn.onClick.AddListener(OnClick);
            }
        }

        protected abstract void OnClick();
    }
}

