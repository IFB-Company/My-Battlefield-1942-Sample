using GameEventBus;
using UnityEngine;

namespace UnityCommonHelpers._Demo._EventBusDemo
{
    public class EventBusHolder : MonoBehaviour
    {
        public EventBus EventBus { get; private set; }

        private void Awake()
        {
            EventBus = new EventBus();
        }
    }
}
