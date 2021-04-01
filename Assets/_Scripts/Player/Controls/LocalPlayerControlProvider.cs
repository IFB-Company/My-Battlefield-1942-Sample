using System.Collections.Generic;
using _Scripts.Player.Controls.Base;
using _Scripts.Static;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Scripts.Player.Controls
{
    public class LocalPlayerControlProvider : MonoBehaviour, IPlayerControlProvider
    {
        [SerializeField] private Joystick _rotationJoystick;
        [SerializeField] private Joystick _moveJoystick;
        
        
        private IControlProvider _rotationControl;
        private IControlProvider _moveControl;
        
        private Dictionary<string, IControlProvider> _controlsDict;

        private void Awake()
        {
            Assert.IsNotNull(_rotationJoystick ,"_rotationJoystick != null");
            Assert.IsNotNull(_moveJoystick ,"_moveJoystick != null");

            InitControls();
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
    }
}
