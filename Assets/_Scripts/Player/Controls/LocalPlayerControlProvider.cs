using System;
using System.Collections.Generic;
using _Scripts.Player.Controls.Base;
using _Scripts.Static;
using _Scripts.Units.Vehicle;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _Scripts.Player.Controls
{
    public class LocalPlayerControlProvider : MonoBehaviour, IPlayerControlProvider
    {
        [SerializeField] private Joystick _rotationJoystick;
        [SerializeField] private Joystick _moveJoystick;
        [SerializeField] private UIButtonControlProvider _uiButtonControlProvider;

        [Space]
        [SerializeField] private Button _vehicleInteractButton;
        [SerializeField] private UnitVehiclePilotController _pilotController;
        [SerializeField] private UnitVehicleDetector _vehicleDetector;
        
        private IControlProvider _rotationControl;
        private IControlProvider _moveControl;
        
        private Dictionary<string, IControlProvider> _controlsDict;

        private void Awake()
        {
            Assert.IsNotNull(_rotationJoystick ,"_rotationJoystick != null");
            Assert.IsNotNull(_uiButtonControlProvider ,"_uiButtonControlProvider != null");
            Assert.IsNotNull(_moveJoystick ,"_moveJoystick != null");
            
            Assert.IsNotNull(_pilotController ,"_pilotController != null");
            Assert.IsNotNull(_vehicleDetector ,"_vehicleDetector != null");
            Assert.IsNotNull(_vehicleInteractButton ,"_vehicleInteractButton != null");
            
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
            _vehicleInteractButton.gameObject.SetActive(true);
            _vehicleInteractButton.onClick.RemoveAllListeners();
            _vehicleInteractButton.onClick.AddListener(() =>
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
            _vehicleInteractButton.gameObject.SetActive(false);
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
            return _uiButtonControlProvider;
        }
    }
}
