using UnityEngine;

namespace _Scripts.CommonStructs
{
    [System.Serializable]
    public struct RangeData
    {
        [SerializeField] private Vector2 _range;
        public float GetValue => Random.Range(_range.x, _range.y);
    }
}