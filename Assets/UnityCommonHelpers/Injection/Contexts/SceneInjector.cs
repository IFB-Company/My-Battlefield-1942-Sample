using UnityEngine;
using Injection;
using System.Linq;

namespace UnityCommonHelpers.Injection.Contexts
{
    public class SceneInjector : MonoBehaviour, IInjectable {
        [SerializeField] protected MonoInjectorBase[] _monoInjectors;
        protected Injector _sceneInjector;

        void OnValidate()
        {
            if(_monoInjectors != null && !_monoInjectors.Any()) {
                _monoInjectors = FindObjectsOfType<MonoInjectorBase>();
            }
        }

        protected virtual void Awake() {
            var globalInjector = GlobalInjector.Instance;

            _sceneInjector = globalInjector == null ? _sceneInjector = new Injector() 
                : _sceneInjector = new Injector(globalInjector);


            InitScene();
        }

        protected virtual void InitScene() {
            _sceneInjector.Bind(this);
            BindMonoInjectors();
        }

        protected void BindMonoInjectors() {
            foreach(var monoInj in _monoInjectors) {
                monoInj.Init(_sceneInjector);
            }
        }

    }
}
