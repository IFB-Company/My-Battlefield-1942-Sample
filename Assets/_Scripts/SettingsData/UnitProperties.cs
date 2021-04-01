using UnityEngine;

namespace _Scripts.SettingsData
{
    [CreateAssetMenu(fileName = "UnitProperties", menuName = "Battle1942/UnitProperties", order = 0)]
    public class UnitProperties : ScriptableObject
    {
        [SerializeField] private float _moveSpeed = 10f;
        public float MoveSpeed => _moveSpeed;

        [SerializeField] private float _rotationSpeed = 10f;
        public float RotationSpeed => _rotationSpeed;
    }
}