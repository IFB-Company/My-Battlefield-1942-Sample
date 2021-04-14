using UnityEngine;

namespace Common.Values
{
    public abstract class ValueWorkerBase : MonoBehaviour
    {
        protected INormalizedValueProvider _valueProvider;
        public virtual void SetValue(INormalizedValueProvider valueProvider)
        {
            _valueProvider = valueProvider;
        }
    }
}
