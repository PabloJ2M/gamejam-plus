using UnityEngine.Events;

namespace UnityEngine.Tutorial.UI
{
    public class Indicator : TutorialBehaviour
    {
        [SerializeField] private RectTransform _area;
        [SerializeField] private RectTransform _arrow;
        [SerializeField, Range(0, 360)] private float _angle;
        [SerializeField, Range(0, 100)] private int _margin;

        [SerializeField] private UnityEvent<bool> _onValueChanged;

        private Canvas _canvas;
        private RectTransform _current;

        protected override void Awake() { base.Awake(); _canvas = GetComponentInParent<Canvas>(); _canvas.worldCamera = Camera.main; }
        private void Start() => _onValueChanged.Invoke(false);
        private void Update() => SetRectPosition();

        protected override void OnPerformeTutorial(TutorialStep step)
        {
            bool useIndicator = step.Interaction != InteractionType.TextOnly;
            _current = useIndicator ? step.onDisplayIndicator.Invoke() : null;
            _onValueChanged.Invoke(useIndicator);
            SetRectPosition();
        }
        private void SetRectPosition()
        {
            if (_current == null) return;

            _area.sizeDelta = _current.rect.size + Vector2.one * _margin;
            _area.position = _current.position;

            bool h = _area.position.x < _canvas.transform.position.x;
            bool v = _area.position.y < _canvas.transform.position.y;
            _arrow.localPosition = new(h ? _area.rect.xMax : _area.rect.xMin, v ? _area.rect.yMax : _area.rect.yMin);
            _arrow.rotation = Quaternion.Euler(v ? 180f : 0f, h ? 180f : 0f, -_angle);
        }
    }
}