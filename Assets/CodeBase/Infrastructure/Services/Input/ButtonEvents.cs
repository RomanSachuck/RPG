using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CodeBase.Services.Input
{
    public class ButtonEvents : Button
    {
        public bool ButtonIsPressed { get; private set; }
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            ButtonIsPressed = true;
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            ButtonIsPressed = false;
        }
    }
}
