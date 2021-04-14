using UnityEngine;

namespace Common
{
    public class GizmosHighlighter : MonoBehaviour
    {
        [SerializeField] private Color _color = Color.red;
        [SerializeField] private float _radius = 1f;
        [SerializeField] private Vector3 _cubeSize = Vector3.one;
        [SerializeField] private bool _isSpherical = false;

        private void OnDrawGizmos()
        {
            Gizmos.color = _color;
            if (_isSpherical)
            {
                Gizmos.DrawSphere(transform.position, _radius);
            }
            else
            {
                Gizmos.DrawCube(transform.position, _cubeSize);
            }
        }
    }
}