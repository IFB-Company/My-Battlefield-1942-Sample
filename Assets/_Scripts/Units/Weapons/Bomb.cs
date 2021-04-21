using _Scripts.Units.Base;
using UnityEngine;

namespace _Scripts.Units.Weapons
{
    public class Bomb : MonoBehaviour, IBullet
    {
        [Range(1, 100)]
        [SerializeField] private int _detectedAmount = 10;
        [SerializeField] private float _explodeRange = 5f;
        [Header("To avoid explosion by himself")]
        [SerializeField] private float _activeTimer = 1f;
        private BattleUnitBase _bulletSender;
        private Collider[] _colliders;
        private WeaponData _weaponData;

        private bool _isActive;

        public void Initialize(BattleUnitBase source, WeaponData weaponData)
        {
            _bulletSender = source;
            _colliders = new Collider[_detectedAmount];
            _weaponData = weaponData;
            _isActive = false;
            
            Invoke(nameof(ActivateBomb), _activeTimer);
        }

        private void ActivateBomb()
        {
            _isActive = true;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!_isActive)
                return;
            
            Debug.Log($"Bomb {gameObject.name} exploded by {other.gameObject.name}!");
            var detected = Physics.OverlapSphereNonAlloc(transform.position, _explodeRange, _colliders);
            foreach (var col in _colliders)
            {
                if (col != null)
                {
                    if (col.TryGetComponent(out HittableObject hittableObject))
                    {
                        Debug.Log($"Bomb {name} damage {hittableObject.name}!");
                        hittableObject.Damage(_bulletSender,
                            _weaponData);
                    }   
                }
            }
            
            Debug.Log($"EXPLODE! By {other.gameObject.name}");
            gameObject.SetActive(false);
        }

        private void OnDrawGizmos()
        {
            var color = Color.red;
            color.a = 0.5f;

            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, _explodeRange);
        }
    }
}
