using UnityEngine;
using UnityEngine.Assertions;

namespace Game
{
    [RequireComponent(typeof(Collider))]
    public abstract class TriggerColliderBase : MonoBehaviour
    {
        protected virtual void Awake()
        {
            var col = GetComponent<Collider>();
            Assert.IsNotNull(col, "col != null");
            if (col != null)
            {
                Assert.IsTrue(col.isTrigger, "col.isTrigger");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            OnTriggered(other);
        }

        protected abstract void OnTriggered(Collider other);
    }  
}