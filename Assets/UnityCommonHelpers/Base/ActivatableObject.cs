using UnityEngine;
using UnityEngine.Events;

namespace UnityCommonHelpers.Base
{
    public abstract class ActivatableObject : MonoBehaviour
    {
        [SerializeField] protected bool _isActiveByDefault = true;

        [SerializeField] protected UnityEvent _onEnable;
        [SerializeField] protected UnityEvent _onDisable;
        
        [Space]
        [Header("Runtime")]
        [SerializeField] protected bool _isActive = true;
        public bool IsActive => _isActive;

        protected virtual void Awake()
        {
            SetActive(_isActiveByDefault);
        }

        public virtual void SetActive(bool isActive)
        {
            _isActive = isActive;

            if (_isActive)
            {
                _onEnable?.Invoke();
            }
            else
            {
                _onDisable?.Invoke();
            }
        }
    }
}
