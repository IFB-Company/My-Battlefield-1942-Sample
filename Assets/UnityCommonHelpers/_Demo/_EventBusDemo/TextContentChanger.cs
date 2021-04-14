using GameEventBus;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace UnityCommonHelpers._Demo._EventBusDemo
{
    public class TextContentChanger : MonoBehaviour
    {
        [SerializeField] private Text _text;
        
        private EventBus _eventBus;
        private void Start()
        {
            Assert.IsNotNull(_text, "_text != null");
            var eventBusHolder = FindObjectOfType<EventBusHolder>();
            Assert.IsNotNull(eventBusHolder, "eventBusHolder != null");
            _eventBus = eventBusHolder.EventBus;
            Assert.IsNotNull(_eventBus, "eventBus != null");
            
            _eventBus.Subscribe<TextContentClickedEvent>(OnTextChanged);
        }

        private void OnDestroy()
        {
            if (_eventBus != null)
            {
                _eventBus.Unsubscribe<TextContentClickedEvent>(OnTextChanged);
            }
        }

        private void OnTextChanged(TextContentClickedEvent textContentClickedEvent)
        {
            if (_text != null)
            {
                _text.color = textContentClickedEvent.Color;
                _text.text = textContentClickedEvent.TextContent;
            }   
        }
    }
}
