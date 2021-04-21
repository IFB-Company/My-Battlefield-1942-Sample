using Common.UI;
using UnityEngine.SceneManagement;

namespace _Scripts.UI.DebugTesting
{
    public class RestartCurrentSceneBtn : ButtonBase
    {
        protected override void OnClick()
        {
            var currentScene = SceneManager.GetActiveScene();
            if (currentScene != null)
            {
                SceneManager.LoadScene(currentScene.name);
            }
        }
    }
}
