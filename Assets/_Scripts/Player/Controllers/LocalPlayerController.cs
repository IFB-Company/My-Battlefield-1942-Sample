using System.Collections.Generic;
using _Scripts.Player.Controllers.Base;
using _Scripts.Player.Controls;
using _Scripts.Player.Controls.Base;
using _Scripts.Static;
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
                var rotationControl =
                    _localPlayerControlProvider.GetControlProviderByName(GameHelper.ControlNames.ROTATION_JOYSTICK);

                var movementControl =
                    _localPlayerControlProvider.GetControlProviderByName(GameHelper.ControlNames.MOVEMENT_JOYSTICK);

                var moveVector = new Vector3(
                    movementControl.GetX,
                    0,
                    movementControl.GetY
                );
                
                _currentBattleUnit.MoveAtFrame(moveVector);
                
                _currentBattleUnit.RotateAtFrame(Vector3.up * rotationControl.GetX);
            }
        }
    }
}
