using _Scripts.Player.Controllers.Base;
using _Scripts.Player.Controls;
using _Scripts.Player.Controls.Base;
using _Scripts.Units.Base;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Player.Controllers
{
    public class LocalPlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private LocalPlayerControlProvider _localPlayerControlProvider;

        [Space]
        [SerializeField] private Vector3 _unitMoveDirection = Vector3.forward;
        
        [Space]
        [Header("Runtime")]
        [SerializeField] private BattleUnitBase _currentBattleUnit;
        public BattleUnitBase CurrentBattleUnit => _currentBattleUnit;

        public IPlayerControlProvider ControlProvider => _localPlayerControlProvider;
        
        private void Awake()
        {
            Assert.IsNotNull(_localPlayerControlProvider, "_localPlayerControlProvider != null");
        }

        private void Update()
        {
            ControlCurrentUnitLoop();
        }

        private void ControlCurrentUnitLoop()
        {
            if (_currentBattleUnit != null)
            {
                if (_localPlayerControlProvider.IsInstantMoveActivated)
                {
                    _currentBattleUnit.MoveAtFrame(_unitMoveDirection);
                }

                var rotationControl = _localPlayerControlProvider.RotationControl;
                _currentBattleUnit.RotateAtFrame(Vector3.up * rotationControl.GetX);
            }
        }
    }
}
