using UnityEngine;
using UnityEngine.Events;

namespace UnityHelpers
{
    public class GameObjectEventHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onEnable;
        [SerializeField] private UnityEvent _onDisable;

        private void OnEnable()
        {
            _onEnable?.Invoke();
        }

        private void OnDisable()
        {
            _onDisable?.Invoke();
        }
    }
}
