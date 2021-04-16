using UnityEngine;

namespace _Scripts.Units.Data
{
    [System.Serializable]
    public struct HittableData
    {
        [SerializeField] private int _currentHp;
        public int CurrentHp => _currentHp;

        [SerializeField] private bool _isDead;
        public bool IsDead => _isDead;

        public HittableData(int currentHp, bool isDead)
        {
            _currentHp = currentHp;
            _isDead = isDead;
        }
    }
}
