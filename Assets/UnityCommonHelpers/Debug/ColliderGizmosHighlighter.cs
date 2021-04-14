using System;
using UnityEngine;

namespace Common
{
    [RequireComponent(typeof(Collider))]
    public class ColliderGizmosHighlighter : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Color _color;
        
        private void OnValidate()
        {
            if (_collider == null)
            {
                _collider = GetComponent<Collider>();
            }
        }

        private void OnDrawGizmos()
        {
            if(_collider == null)
                return;

            Gizmos.color = _color;
            var scale = transform.lossyScale;
            if(_collider is BoxCollider boxCol)
            {
                var boxSize = boxCol.size;
                var size = new Vector3(
                    boxSize.x * scale.x,
                    boxSize.y * scale.y,
                    boxSize.z * scale.z
                    );
                Gizmos.DrawCube(transform.position + boxCol.center, size);
            } 
            else if (_collider is SphereCollider sphereCol)
            {
                var sphereRadius = sphereCol.radius;
                Gizmos.DrawSphere(transform.position + sphereCol.center, sphereRadius * scale.x
                );
                
            }
        }
    }
}

