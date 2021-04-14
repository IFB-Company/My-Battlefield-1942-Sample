using Common;
using Injection;
using UnityEngine;
using UnityEngine.Assertions;

namespace UnityCommonHelpers._Demo._InjectorDemo
{
    public class CubeRotator : MonoBehaviour, IInjectable
    {
        [SerializeField] private TransformRotator _transformRotator;
        
        [Inject] private Calculator _calculator;
        [Inject] private CubeRotatorData _cubeRotatorData;
        
        private void Awake()
        {
            Assert.IsNotNull(_transformRotator, "_transformRotator != null");
        }

        private void Start()
        {
            int x = 5;
            int y = 15;
            print($"{x} + {y} = {_calculator.Addition(x, y)}");
            print($"{x} - {y} = {_calculator.Subtraction(x, y)}");
            
            _transformRotator.Direction = _cubeRotatorData.RotationAngle;
            _transformRotator.RotSpeed = _cubeRotatorData.RotationSpeed;
            _transformRotator.IsEnabled = true;
        }
    }
}
