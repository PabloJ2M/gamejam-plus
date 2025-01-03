using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class ScreenDraw : DragBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private LineRenderer _render;
        [SerializeField, Range(0, 1)] private float _distance;
        [SerializeField] private int _distanceLimit;

        [SerializeField] private UnityEvent<float> _onUpdateDistance;

        private Vector2 _lastPosition;
        private float _limit;

        protected override void Start()
        {
            float screenWidth = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.width;
            float scaleFactor = screenWidth / 1920f;
            _limit = _distanceLimit * scaleFactor;
            base.Start();
        }
        protected override void OnSelect()
        {
            if (IsPointerOverUI()) return;
            _render.positionCount = 0;
            base.OnSelect();
            ForceUpdate();
        }

        protected override void OnUpdateSelection(Vector2 screenPosition)
        {
            float traveled = _render.positionCount * _distance;
            if (traveled >= _limit) return;

            Vector2 worldPos = _camera.ScreenToWorldPoint(screenPosition);
            Vector2 direction = _lastPosition - worldPos;
            if (direction.magnitude < _distance) return;

            _render.positionCount++;
            _render.SetPosition(_render.positionCount - 1, worldPos);
            _onUpdateDistance.Invoke(1f - (traveled / _limit));
            _lastPosition = worldPos;
        }
    }
}