using System;
using UnityEngine;
using UnityEngine.Events;

namespace UnityCommonHelpers
{
    public abstract class GameStateChangerBase<TEnum> : MonoBehaviour where TEnum : System.Enum
    {
        [System.Serializable]
        protected class StateContainer
        {
            [SerializeField] protected TEnum _stateType;
            public TEnum StateType => _stateType;

            [SerializeField] protected UnityEvent _onActivate;

            [SerializeField] protected GameObject[] _relativeObjects;

            public void SetActiveRelativeObjects(bool isActive)
            {
                foreach (var relObj in _relativeObjects)
                {
                    if (relObj != null)
                    {
                        relObj.SetActive(isActive);
                    }
                }
            }

            public void SetActive(bool isActive)
            {
                if (isActive)
                {
                    _onActivate?.Invoke();
                }

                SetActiveRelativeObjects(isActive);
            }
        }

        [SerializeField] protected TEnum _defaultState;
        [SerializeField] protected StateContainer[] _stateContainers;
        [SerializeField] protected bool _isInitDefaultStateOnAwake = true;

        [Space]
        [SerializeField] protected TEnum _currentState;

        public event Action<TEnum> OnStateChanged;

        protected virtual void Awake()
        {
            if (_isInitDefaultStateOnAwake)
                SetState(_defaultState);
        }

        public virtual void SetState(TEnum stateType)
        {
            StateContainer stateContainer = null;
            foreach (var sCon in _stateContainers)
            {
                if (sCon.StateType.Equals(stateType))
                {
                    stateContainer = sCon;
                    stateContainer.SetActive(true);
                }
                else
                {
                    sCon.SetActive(false);
                }
            }

            if (stateContainer == null)
            {
                Debug.LogError($"{nameof(StateContainer)} with type {stateType} is missing!");
                return;
            }


            _currentState = stateType;
            OnStateChanged?.Invoke(stateType);
        }
    }
}