using Common.UI;
using UnityEngine;
using UnityEngine.Assertions;

namespace UnityCommonHelpers._Demo._EventBusDemo
{
    public class HelloButton : ButtonBase
    {
        [SerializeField] private string _content;
        [SerializeField] private Color _color;

        private EventBusHolder _eventBusHolder;
        
        protected override void Awake()
        {
            base.Awake();

            _eventBusHolder = FindObjectOfType<EventBusHolder>();
            Assert.IsNotNull(_eventBusHolder, "_eventBusHolder != null");
        }

        protected override void OnClick()
        {
            var eventBus = _eventBusHolder.EventBus;
            Assert.IsNotNull(eventBus, "eventBus != null");
            eventBus?.Publish(new TextContentClickedEvent(_content, _color));
        }
    }
}
