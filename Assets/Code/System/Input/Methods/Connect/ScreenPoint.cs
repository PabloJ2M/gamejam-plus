using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace UnityEngine.InputSystem
{
    [RequireComponent(typeof(RectTransform))]
    public class ScreenPoint : ItemBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private ScreenConnect _manager;

        protected override void Awake() { base.Awake(); _manager = GetComponentInParent<ScreenConnect>(); }
        private void SendPoint() { if (_manager.IsDragging) _manager.AddPoint(this); }

        public void OnPointerEnter(PointerEventData eventData) => SendPoint();
        public void OnPointerExit(PointerEventData eventData) => SendPoint();
    }
}