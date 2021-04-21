using System;
using _Scripts.Location;
using UnityEngine;

namespace _Scripts.Units
{
    public class DisappearBehaviour : MonoBehaviour
    {
        [SerializeField] protected float _time = 2f;
        [SerializeField] protected bool _isReturnToPool = true;
        [SerializeField] protected string _poolName;

        protected virtual void OnValidate()
        {
            _poolName = gameObject.name;
        }

        protected virtual void OnEnable()
        {
            Invoke(nameof(DisappearOnTime), _time);
        }

        protected virtual void DisappearOnTime()
        {
            gameObject.SetActive(false);
        }

        protected virtual void OnDisable()
        {
            if (_isReturnToPool)
            {
                var pool = SceneGamePool.Instance;
                if (pool != null)
                {
                    pool.AddObjectInPool(_poolName, this.gameObject);
                }
            }
        }
    }
}
