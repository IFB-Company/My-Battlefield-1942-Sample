using System;
using _Scripts.Location;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Units.Weapons
{
    public class ProjectileMover : MonoBehaviour
    {
        [SerializeField] private string _poolName;
        [SerializeField] private float _lifeTime = 5f;
        [SerializeField] private float _moveSpeed = 10f;
        

        private void OnEnable()
        {
            Invoke(nameof(DisableProcess), _lifeTime);
        }

        private void DisableProcess()
        {
            gameObject.SetActive(false);
            if (SceneGamePool.Instance != null)
            {
                SceneGamePool.Instance.AddObjectInPool(_poolName, this.gameObject);
            }
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * _moveSpeed * Time.deltaTime);
        }
    }
}
