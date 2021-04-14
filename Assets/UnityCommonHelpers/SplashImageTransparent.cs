using UnityEngine;
using UnityEngine.UI;

namespace Common.Visual
{
    [RequireComponent(typeof(Image))]
    public class SplashImageTransparent : MonoBehaviour
    {

        [SerializeField] private float _appearSpeed = 6f;
        [SerializeField] private float _timeToShow = 1f;
        
        [Range(0f,1f)]
        [SerializeField] private float _showAlpha = 0.5f;

        private float _targetAlpha = 0f;
        private bool _isShowed;
        private float _showTimer;
        private Image _img;

        private void Awake()
        {
            _img = GetComponent<Image>();
        }

        public void Show()
        {
            _showTimer = 0f;
            _targetAlpha = _showAlpha;
            _isShowed = true;
        }

        private void Update()
        {
            if (_img == null)
                return;

            var currentColor = _img.color;
            var currentAlpha = Mathf.Lerp(currentColor.a, _targetAlpha, _appearSpeed * Time.deltaTime);
            _img.color = new Color(currentColor.r, currentColor.g, currentColor.b, currentAlpha);

            if (_showTimer > _timeToShow)
            {
                _showTimer = 0f;
                _targetAlpha = 0f;
                _isShowed = false;
            }

            _showTimer += Time.deltaTime;
        }
    } 
}

