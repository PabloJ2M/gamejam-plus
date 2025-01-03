using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem
{
    [RequireComponent(typeof(RectTransform))]
    public class ScreenPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private ScreenConnect _manager;
        private Transform _transform;

        public Vector2 Position => _transform.position;

        private void Awake() { _transform = transform; _manager = GetComponentInParent<ScreenConnect>(); }
        private void SendPoint() { if (_manager.IsDragging) _manager.AddPoint(this); }

        public void OnPointerEnter(PointerEventData eventData) => SendPoint();
        public void OnPointerExit(PointerEventData eventData) => SendPoint();
    }
}