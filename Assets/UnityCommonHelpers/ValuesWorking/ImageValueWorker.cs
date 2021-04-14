using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common.Values
{
    [RequireComponent(typeof(Image))]
    public class ImageValueWorker : ValueWorkerBase
    {
        [SerializeField] private bool _isSmoothChange = true;
        [SerializeField] private float _changeSpeed = 6f;
        private Image _img;
        private void Awake()
        {
            _img = GetComponent<Image>();
        }

        private void Update()
        {
            if (_img == null)
                return;
            if (_valueProvider == null)
                return;

            if (_isSmoothChange)
            {
                _img.fillAmount = Mathf.Lerp(_img.fillAmount, _valueProvider.GetNormalizedValue(),
                    _changeSpeed * Time.deltaTime);
            }
            else
            {
                _img.fillAmount = _valueProvider.GetNormalizedValue();
            }
        }
    }
}
