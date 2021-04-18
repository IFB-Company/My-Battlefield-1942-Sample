using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Player.Controls.Base;
using _Scripts.Player.Controls.Enums;
using _Scripts.Static;
using _Scripts.Units.Base;
using _Scripts.Units.Vehicle;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _Scripts.Player.Controls
{
    public class LocalPlayerControlProvider : MonoBehaviour, IPlayerControlProvider
    {
        [System.Serializable]
        private class ControlProviderByUnitType
        {
            [SerializeField] private UnitType _unitType;
            public UnitType UnitType => _unitType;
            
            [SerializeField] private UIButtonControlProvider _uiButtonControlProvider;
            public UIButtonControlProvider UIButtonControlProvider => _uiButtonControlProvider;
        }
        
        [SerializeField] private Joystick _rotationJoystick;
        [SerializeField] private Joystick _moveJoystick;
        [SerializeField] private ControlProviderByUnitType[] _controlProviders;
        

        [Space]
        [SerializeField] private UnitVehiclePilotController _pilotController;
        [SerializeField] private UnitVehicleDetector _vehicleDetector;
        
        private IControlProvider _rotationControl;
        private IControlProvider _moveControl;
        
        private Dictionary<string, IControlProvider> _controlsDict;
        private UIButtonControlProvider _currentButtonProvider;

        private void Awake()
        {
            Assert.IsNotNull(_rotationJoystick ,"_rotationJoystick != null");
            Assert.IsNotNull(_moveJoystick ,"_moveJoystick != null");
            
            Assert.IsNotNull(_pilotController ,"_pilotController != null");
            Assert.IsNotNull(_vehicleDetector ,"_vehicleDetector != null");

            _vehicleDetector.OnVehicleDetected += VehicleDetectorOnVehicleDetected;
            _vehicleDetector.OnNoVehiclesNearly += VehicleDetectorOnNoVehiclesNearly;

            InitControls();
        }
        

        private void OnDestroy()
        {
            _vehicleDetector.OnVehicleDetected -= VehicleDetectorOnVehicleDetected;
            _vehicleDetector.OnNoVehiclesNearly -= VehicleDetectorOnNoVehiclesNearly;
        }

        private void VehicleDetectorOnVehicleDetected(BattleVehicleBase vehicle)
        {
            //TODO: FIX BUG WITH INTERACT 
            var vehicleInteractBtn = _currentButtonProvider.GetButtonByType(ButtonType.VEHICLE_INTERACT);
            Assert.IsNotNull(vehicleInteractBtn, "vehicleInteractBtn != null");
            vehicleInteractBtn.gameObject.SetActive(true);
            vehicleInteractBtn.onClick.RemoveAllListeners();
            vehicleInteractBtn.onClick.AddListener(() =>
            {
                if (_pilotController.IsOnVehicle)
                {
                    _pilotController.LeaveFromVehicle();
                }
                else
                {
                    _pilotController.EnterInVehicle(vehicle);   
                }
            });
        }
        
        private void VehicleDetectorOnNoVehiclesNearly()
        {
            var vehicleInteractBtn = _currentButtonProvider.GetButtonByType(ButtonType.VEHICLE_INTERACT);
            Assert.IsNotNull(vehicleInteractBtn, "vehicleInteractBtn != null");
            vehicleInteractBtn.gameObject.SetActive(false);
        }

        private void InitControls()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            _rotationControl = new JoystickAndMouseControlProvider(_rotationJoystick);
            _moveControl = new JoystickAndKeyboardControlProvider(_moveJoystick);
#else
            _rotationControl = new JoystickControlProvider(_rotationJoystick);
            _moveControl = new JoystickAndKeyboardControlProvider(_moveJoystick);
#endif

            _controlsDict = new Dictionary<string, IControlProvider>()
            {
                {GameHelper.ControlNames.MOVEMENT_JOYSTICK, _moveControl},
                {GameHelper.ControlNames.ROTATION_JOYSTICK, _rotationControl}
            };

        }
        

        public IControlProvider GetControlProviderByName(string controlProviderName)
        {
            if (_controlsDict.TryGetValue(controlProviderName, out IControlProvider provider))
            {
                return provider;
            }

            return GameHelper.NullObjects.NullControlProvider;
        }

        public IButtonControlProvider GetButtonControlProvider()
        {
            return _currentButtonProvider;
        }

        public void UpdateUiByUnit(BattleUnitBase battleUnit)
        {
            var unitType = battleUnit.UnitType;
            var buttonControlProvider = _controlProviders.FirstOrDefault(bc => bc.UnitType == unitType);
            if (buttonControlProvider == null)
            {
                Debug.LogError($"There are no UI for unitType : {unitType}!");
                return;
            }

            foreach (var controlContainer in _controlProviders)
            {
                if (controlContainer != null)
                {
                    controlContainer.UIButtonControlProvider.gameObject.SetActive(controlContainer == buttonControlProvider);
                }
            }
            
            Assert.IsNotNull(buttonControlProvider.UIButtonControlProvider, "buttonControlProvider.UIButtonControlProvider != null");
            _currentButtonProvider = buttonControlProvider.UIButtonControlProvider;

        }
    }
}
