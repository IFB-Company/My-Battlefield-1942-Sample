using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public class DisablerByTime : MonoBehaviour
    {
        [SerializeField] private float _timeToDisable = 0.5f;
        [SerializeField] private bool _isDestroy;
        private WaitForSeconds _waiting;

        private Coroutine _deactivationCoroutine;
        
        private void Awake()
        {
            _waiting = new WaitForSeconds(_timeToDisable);
        }

        private void OnEnable()
        {
            if (_deactivationCoroutine != null)
                return;
            _deactivationCoroutine = StartCoroutine(StartDeactivation());
        }
        

        private IEnumerator StartDeactivation()
        {
            yield return _waiting;
            
            if (_isDestroy)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
                _deactivationCoroutine = null;
            }
        }
    }
}
