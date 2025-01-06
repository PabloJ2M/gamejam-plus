using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace UnityEngine.InputSystem
{
    [RequireComponent(typeof(RectTransform))]
    public class ScreenPoint : MonoBehaviour, IPoolItem, IPointerEnterHandler, IPointerExitHandler
    {
        private ScreenConnect _manager;
        private Transform _transform;

        public IObjectPool<IPoolItem> Pool { get; set; }
        public GameObject Object => _transform.gameObject;
        public bool IsActive { set => _transform.gameObject.SetActive(value); }
        public Vector3 Position { get => _transform.position; set => _transform.position = value; }

        private void Awake() { _transform = transform; _manager = GetComponentInParent<ScreenConnect>(); }
        private void SendPoint() { if (_manager.IsDragging) _manager.AddPoint(this); }

        public void OnPointerEnter(PointerEventData eventData) => SendPoint();
        public void OnPointerExit(PointerEventData eventData) => SendPoint();
    }
}