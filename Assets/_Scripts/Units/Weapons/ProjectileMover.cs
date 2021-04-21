﻿using System;
using _Scripts.Location;
using UnityEngine;

namespace _Scripts.Units.Weapons
{
    public class ProjectileMover : MonoBehaviour
    {
        [SerializeField] private string _poolName;
        [Header("If ms or lifetime is zero = infinity")]
        [SerializeField] private float _lifeTime = 5f;
        [SerializeField] private float _moveSpeed = 10f;

        [SerializeField] private TrailRenderer[] _trailRenderers;

        private void OnValidate()
        {
            _trailRenderers = GetComponentsInChildren<TrailRenderer>(true);
        }

        private void OnEnable()
        {
            if (!Mathf.Approximately(_lifeTime, 0f))
            {
                Invoke(nameof(DisableProcess), _lifeTime);   
            }
        }

        private void DisableProcess()
        {
            foreach (var trail in _trailRenderers)
            {
                if (trail != null)
                {
                    trail.Clear();
                }
            }
            gameObject.SetActive(false);
            
        }

        private void OnDisable()
        {
            if (SceneGamePool.Instance != null)
            {
                SceneGamePool.Instance.AddObjectInPool(_poolName, this.gameObject);
            }
        }

        private void Update()
        {
            if (!Mathf.Approximately(_moveSpeed, 0f))
            {
                transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);   
            }
        }
    }
}
