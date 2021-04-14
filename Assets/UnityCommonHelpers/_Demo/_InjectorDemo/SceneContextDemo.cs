using System;
using Injection;
using UnityEngine;

namespace UnityCommonHelpers._Demo._InjectorDemo
{
    /// <summary>
    /// This class is responsibility for setup all dependencies in container
    /// then it setup any dependencies with an attribute '[Inject]' inside objects that has it.
    /// </summary>
    public class SceneContextDemo : MonoBehaviour
    {
        [SerializeField] private CubeRotatorData _cubeRotatorData;
        [SerializeField] private CubeRotator _cubeRotator;
        
        private Injector _injector;
        private void Awake()
        {
            _injector = new Injector();
            
            // Register dependencies
            _injector.Bind(new Calculator());
            _injector.Bind(_cubeRotatorData);

            // Bind object that requires dependencies
            _injector.Bind(_cubeRotator);
            
            // Setup all references by registered dependencies
            _injector.PostBindings();
        }
    }
}
