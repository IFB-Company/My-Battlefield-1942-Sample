using UnityEngine;

namespace _Scripts.Units
{
    public class FootmanBattleUnit : GroundBattleUnit
    {
        private float _xRotation = 0f;
        [SerializeField] private Transform _faceObj;
        protected override void MovementLoop()
        {
            base.MovementLoop();

            if (_targetRotDir.HasValue)
            {
                _xRotation -= _targetRotDir.Value.y;
                float maxAngle = 90f;
                _xRotation = Mathf.Clamp(_xRotation, -maxAngle, maxAngle);
                _faceObj.localRotation = Quaternion.Euler(_xRotation, 0f,0f);
            }
        }
    }
}
