using _Scripts.Units.Base;
using UnityEngine;
using UnityEngine.AI;

namespace _Scripts.Units
{
    public class GroundBattleUnit : BattleUnitBase
    {
        [SerializeField] protected NavMeshAgent _navMeshAgent;
        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        

        public override void MoveAtFrame(Vector3 dir)
        {
            if (_navMeshAgent != null)
            {
                _navMeshAgent.Move(transform.forward * _unitProperties.MoveSpeed * Time.deltaTime);
            }
        }

        public override void RotateAtFrame(Vector3 dir)
        {
            transform.Rotate(dir * _unitProperties.RotationSpeed * Time.deltaTime);
        }
    }
}
