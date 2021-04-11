using System;
using _Scripts.Player.Controls.Base;
using _Scripts.Player.Controls.Enums;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace _Scripts.Player.Controls
{
    public class UIButtonControlProvider : MonoBehaviour, IButtonControlProvider
    {
        [System.Serializable]
        private class ButtonContainer
        {
            [SerializeField] private ButtonType _buttonType;
            public ButtonType ButtonType => _buttonType;
            
            [SerializeField] private Button _button;
            public Button Button => _button;
            
        }

        [SerializeField] private ButtonContainer[] _buttonContainers;

        public event Action<ButtonType> OnButtonPressedEvent;

        private void Awake()
        {
            Assert.IsTrue(_buttonContainers.Length > 0, "_buttonContainers.Length > 0");
            
        }

        private void Start()
        {
            InitButtons();
        }

        private void InitButtons()
        {
            foreach (var btnContainer in _buttonContainers)
            {
                Assert.IsNotNull(btnContainer.Button, "btnContainer.Button != null");
                Assert.IsTrue(btnContainer.ButtonType != ButtonType.NONE,
                    "btnContainer.ButtonType != ButtonType.NONE");
                
                btnContainer.Button.onClick.AddListener(() => OnButtonClicked(btnContainer));
            }
        }

        private void OnButtonClicked(ButtonContainer buttonContainer)
        {
            FirePress(buttonContainer.ButtonType);
        }

        private void FirePress(ButtonType buttonType)
        {
            OnButtonPressedEvent?.Invoke(buttonType);
        }
        

#if UNITY_EDITOR || UNITY_STANDALONE
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                FirePress(ButtonType.FIRE);
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                FirePress(ButtonType.JUMP);
            }
        } 
#endif
        
        
    }
}
