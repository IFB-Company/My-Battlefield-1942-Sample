using UnityEngine;

namespace Common.UI
{
    public class ExitButton : ButtonBase
    {
        protected override void OnClick()
        {
            Application.Quit();
        }
    }
}