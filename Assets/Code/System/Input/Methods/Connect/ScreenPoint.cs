using UnityEngine.Pool;

namespace UnityEngine.InputSystem
{
    [RequireComponent(typeof(RectTransform))]
    public class ScreenPoint : ItemBehaviour
    {
        private RectTransform _area;
        private ScreenConnect _manager;

        protected override void Awake()
        {
            base.Awake();
            _area = _transform as RectTransform;
            _manager = GetComponentInParent<ScreenConnect>();
        }
        private void Update()
        {
            if (!_manager.IsDragging) return;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_area, _manager.Input, _manager.Camera, out Vector2 point))
            {
                if (!_area.rect.Contains(point)) return;
                _manager?.AddPoint(this);
            }
        }
    }
}