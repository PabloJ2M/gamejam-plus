using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class ScreenConnect : DragBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private RectTransform _container;
        [SerializeField] private LineRenderer _renderer;
        [SerializeField] private UnityEvent _onComplete;

        public bool IsDragging => _isDragging && !_isLocked;

        private List<ScreenPoint> _points = new();
        private bool _isLocked;

        protected override void OnEnable() { base.OnEnable(); _renderer.positionCount = 1; _points.Clear(); }
        protected override void OnSelect() { base.OnSelect(); _renderer.positionCount = 1; _points.Clear(); }
        protected override void OnDeselect() { base.OnDeselect(); _isDragging = false; if (!_isLocked) _renderer.positionCount = 0; }
        public void UnlockDrag() { _isLocked = false; _renderer.positionCount = 0; }

        protected override void OnUpdateSelection(float2 screenPosition)
        {
            int count = _renderer.positionCount;
            if (_isLocked || count > _container.childCount) return;
            
            _renderer.SetPosition(count - 1, _camera.ScreenToWorldPoint((Vector2)screenPosition));
        }
        public void AddPoint(ScreenPoint newPoint)
        {
            if (_points.Contains(newPoint)) return;
            _points.Add(newPoint);

            int count = _renderer.positionCount;
            _renderer.SetPosition(count - 1, newPoint.Position);

            if (count + 1 <= _container.childCount) { _renderer.positionCount++; ForceUpdate(); return; }
            _onComplete.Invoke();
            _isLocked = true;
            OnDeselect();
        }
    }
}