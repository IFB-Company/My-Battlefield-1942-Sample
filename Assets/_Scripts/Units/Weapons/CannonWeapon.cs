using UnityEngine;

namespace _Scripts.Units.Weapons
{
    public class CannonWeapon : StandartWeapon
    {
        [SerializeField] private float _cannonPower = 1000f;
        
        protected override void FireProcess()
        {
            base.FireProcess();

            if (_lastBullet != null)
            {
                if (_lastBullet.TryGetComponent(out Rigidbody rb))
                {
                    rb.velocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                    
                    rb.AddForce(_aimAnchor.transform.forward * _cannonPower);
                }
            }
            
        }
    }
}
