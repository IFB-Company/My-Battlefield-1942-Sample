using GameEventBus.Events;
using UnityEngine;

namespace UnityCommonHelpers._Demo._EventBusDemo
{
    public class TextContentClickedEvent : EventBase
    {
        public string TextContent { get; private set; }
        public Color Color { get; private set; }
        
        public TextContentClickedEvent(string textContent, Color color)
        {
            this.TextContent = textContent;
            this.Color = color;
        }
    }
}
