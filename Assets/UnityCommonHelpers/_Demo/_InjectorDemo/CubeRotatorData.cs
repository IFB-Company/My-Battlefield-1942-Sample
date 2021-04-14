using Injection;
using UnityEngine;


namespace UnityCommonHelpers._Demo._InjectorDemo
{
    [CreateAssetMenu(fileName = "CubeRotatorData",menuName = "CommonHelpers_Demo/CubeRotatorData")]
    public class CubeRotatorData : ScriptableObject, IInjectable
    {
        [SerializeField] private float _rotationSpeed = 10f;
        public float RotationSpeed => _rotationSpeed;
        
        [SerializeField] private Vector3 _rotationAngle = Vector3.up;
        public Vector3 RotationAngle => _rotationAngle;
    }
}
