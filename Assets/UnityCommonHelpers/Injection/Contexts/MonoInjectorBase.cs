using UnityEngine;
using Injection;

namespace UnityCommonHelpers.Injection.Contexts {
    public abstract class MonoInjectorBase : MonoBehaviour, IInjectable {

        protected Injector _selfInjector;

        public virtual void Init(Injector injector) {
            _selfInjector = new Injector(injector);

            _selfInjector.Bind(this);

            _selfInjector.PostBindings();
        }
    }
}
