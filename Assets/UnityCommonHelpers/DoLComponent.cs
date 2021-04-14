using UnityEngine;

namespace Common
{
    public class DoLComponent : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}

