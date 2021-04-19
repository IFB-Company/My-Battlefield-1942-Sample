using System;
using _Scripts.Player.Controllers.Base;
using _Scripts.Player.Controls;
using _Scripts.Player.Controls.Base;
using _Scripts.Player.Controls.Enums;
using _Scripts.Static;
using _Scripts.Units;
using _Scripts.Units.Base;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Player.Controllers
{
    public class LocalPlayerController : MonoBehaviour, IPlayerController
    {
        [SerializeField] private LocalPlayerControlProvider _localPlayerControlProvider;
        [SerializeField] private FootmanBattleUnit _footmanBattleUnit;

        [Space]
        [Header("Runtime")]
        [SerializeField] private BattleUnitBase _currentBattleUnit;
        

        public FootmanBattleUnit FootmanBattleUnit => _footmanBattleUnit;
        public BattleUnitBase CurrentBattleUnit => _currentBattleUnit;

        public IPlayerControlProvider ControlProvider => _localPlayerControlProvider;

        private IButtonControlProvider _buttonControlProvider;

        public event Action<BattleUnitBase> OnCurrentUnitChanged; 

        private void OnValidate()
        {
            if (_footmanBattleUnit == null)
            {
                _footmanBattleUnit = GetComponentInChildren<FootmanBattleUnit>();
            }
        }

        private void Awake()
        {
            Assert.IsNotNull(_localPlayerControlProvider, "_localPlayerControlProvider != null");
            Assert.IsNotNull(_footmanBattleUnit, "_footmanBattleUnit != null");
            

            OnCurrentUnitChanged += UpdateUiByUnit;

        }

        private void Start()
        {
            if (_currentBattleUnit != null)
            {
                OnCurrentUnitChanged?.Invoke(_currentBattleUnit);
            }
        }


        private void OnDestroy()
        {
            if (_buttonControlProvider != null)
            {
                _buttonControlProvider.OnButtonPressedEvent -= ButtonProviderOnButtonPressedEvent;   
            }
            OnCurrentUnitChanged -= UpdateUiByUnit;
        }
        
        
        private void UpdateUiByUnit(BattleUnitBase obj)
        {
            
            _localPlayerControlProvider.UpdateUiByUnit(obj);
            
            if (_buttonControlProvider != null)
            {
                _buttonControlProvider.OnButtonPressedEvent -= ButtonProviderOnButtonPressedEvent;
            }
            
            _buttonControlProvider = _localPlayerControlProvider.GetButtonControlProvider();
            _buttonControlProvider.OnButtonPressedEvent += ButtonProviderOnButtonPressedEvent;
            
        }

        private void ButtonProviderOnButtonPressedEvent(ButtonType btnType)
        {
            if (_currentBattleUnit != null)
            {
                _currentBattleUnit.HandleControlByButtonType(btnType);
            }
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

                var rotVector = new Vector3(
                    rotationControl.GetX,
                    rotationControl.GetY,
                    0
                );
                
                _currentBattleUnit.RotateAtFrame(rotVector);
            }
        }
        
        public void SetCurrentBattleUnit(BattleUnitBase battleUnit)
        {
            _currentBattleUnit = battleUnit;
            OnCurrentUnitChanged?.Invoke(_currentBattleUnit);
        }
    }
}
