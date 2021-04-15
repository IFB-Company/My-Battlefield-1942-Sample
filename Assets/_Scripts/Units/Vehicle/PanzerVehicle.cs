using UnityEngine;

namespace _Scripts.Units.Vehicle
{
    public class PanzerVehicle : BattleVehicleBase
    {
        [SerializeField] private float _torque = 200f;
        [SerializeField] private Rigidbody _leadRb;
        [SerializeField] private ConstantForce _constantForce;
        protected virtual void OnValidate()
        {
            _leadRb = GetComponent<Rigidbody>();
            _constantForce = GetComponent<ConstantForce>();
        }
        
        protected override void Awake()
        {
            base.Awake();
        }

        public override void MoveAtFrame(Vector3 dir)
        {
            _constantForce.force = Vector3.forward * dir.z * _torque;
        }

        public override void RotateAtFrame(Vector3 dir)
        {
            
        }
    }
}
