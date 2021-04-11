using System;
using UnityEngine;

namespace _Scripts.Units
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _checkRadius = 0.2f;
        
        [Space]
        [Header("Runtime")]
        [SerializeField] private bool _isGrounded = false;
        

        private void Update()
        {
            CheckGroundLoop();
        }

        private void CheckGroundLoop()
        {
            _isGrounded = Physics.CheckSphere(transform.position, _checkRadius, _groundLayer);
        }

        public bool IsGrounded()
        {
            return _isGrounded;
        }

        private void OnDrawGizmos()
        {
            var color = Color.blue;
            color.a = 0.5f;

            Gizmos.color = color;
            
            Gizmos.DrawSphere(transform.position, _checkRadius);
        }
    }
}
