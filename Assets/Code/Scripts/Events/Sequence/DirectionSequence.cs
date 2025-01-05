using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UI;

namespace Events.Sequence
{
    public class DirectionSequence : MonoBehaviour
    {
        [System.Serializable] public struct DirectionHandler
        {
            public AdvancedOrientation orient;
            public Sprite icon;
        }

        [SerializeField] private RectTransform _container;
        [SerializeField] private Color _normal, _disable;
        [SerializeField] private int _inScreen, _displacement;

        [SerializeField] private DirectionHandler[] _values;

        [SerializeField] private UnityEvent<AdvancedOrientation> _onDirectionChanged;
        [SerializeField] private UnityEvent _onComplete;

        private Image[] _icons;
        private AdvancedOrientation[] _directions;

        private Vector2 _position;
        private bool _isComplete;
        private int _current;

        private void Awake() { _icons = _container.GetComponentsInChildren<Image>(); _directions = new AdvancedOrientation[_icons.Length]; }
        private void Update() => _container.anchoredPosition = Vector2.Lerp(_container.anchoredPosition, _position, 10 * Time.deltaTime);
        public void Start()
        {
            _position = Vector2.zero;

            for (int i = 0; i < _icons.Length; i++) {
                int r = Random.Range(0, _values.Length);
                _directions[i] = _values[r].orient;
                _icons[i].sprite = _values[r].icon;
                _icons[i].color = _normal;
            }

            _current = 0;
            _isComplete = false;
            _onDirectionChanged.Invoke(_directions[_current]);
        }

        public void CompleteDirection()
        {
            if (_isComplete) return;
            if (_current < _container.childCount - _inScreen) _position += Vector2.left * _displacement;
            _current++;

            if (_current >= _directions.Length) { _isComplete = true; _onComplete.Invoke(); return; }
            for (int i = 0; i < _icons.Length; i++) _icons[i].color = i < _current ? _disable : _normal;
            _onDirectionChanged.Invoke(_directions[_current]);
        }
    }
}