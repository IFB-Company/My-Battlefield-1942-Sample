using _Scripts.Player.Controls.Base;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _Scripts.Player.Controls
{
    public class LocalPlayerControlProvider : MonoBehaviour, IPlayerControlProvider
    {
        [SerializeField] private Joystick _rotationJoystick;
        [SerializeField] private Button _instantMoveBtn;

        [Space]
        [Header("Runtime")]
        [SerializeField] private bool _isInstantMoveActivated;
        public bool IsInstantMoveActivated => _isInstantMoveActivated;
        
        public IControlProvider RotationControl { get; private set; }

        private void Awake()
        {
            Assert.IsNotNull(_rotationJoystick ,"_rotationJoystick != null");
            Assert.IsNotNull(_instantMoveBtn ,"_instantMoveBtn != null");
            
            _instantMoveBtn.onClick.AddListener(OnInstantMoveButtonClicked);
            InitControls();
        }

        private void InitControls()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            RotationControl = new JoystickAndMouseControlProvider(_rotationJoystick);
#else
            RotationControl = new JoystickControlProvider(_rotationJoystick);
#endif
        }

        private void OnInstantMoveButtonClicked()
        {
            _isInstantMoveActivated = !_isInstantMoveActivated;
        }
    }
}
