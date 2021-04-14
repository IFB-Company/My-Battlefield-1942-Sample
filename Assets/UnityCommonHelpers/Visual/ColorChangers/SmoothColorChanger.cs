using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Common.Visual
{
    public class SmoothColorChanger : ColorChangerBase
    {
        [SerializeField] private ColorChangerBase _affectedChanger;
        [SerializeField] private float _speed = 5f;
        [SerializeField] private bool _isEnable = true;
        [SerializeField] private bool _isInitBasicColorAtStart = true;
        [SerializeField] private Color _currentTargetColor;
        private void Awake()
        {
            Assert.IsNotNull(_affectedChanger);
            if (_affectedChanger != null && _isInitBasicColorAtStart)
            {
                _currentTargetColor = _affectedChanger.GetColor();
            }
        }

        public override void SetColor(Color color)
        {
            _currentTargetColor = color;
        }

        public override Color GetColor()
        {
            return _currentTargetColor;
        }

        private void Update()
        {
            if (!_isEnable)
                return;

            if (_affectedChanger == null)
                return;
            
            Color currentColorFromAffected = _affectedChanger.GetColor();
            Color changedColor = Color.Lerp(currentColorFromAffected, _currentTargetColor, _speed * Time.deltaTime);
            _affectedChanger.SetColor(changedColor);
        }
    }
}
