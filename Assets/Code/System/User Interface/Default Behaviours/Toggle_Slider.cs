using UnityEngine.Events;
using Unity.Mathematics;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Slider))]
    public class Toggle_Slider : MonoBehaviour, IToggle
    {
        [SerializeField] private ActivationGroup _group;
        [SerializeField] private RectTransform _areaRect, _targetRect;
        [SerializeField] private Orientation _direction;
        [SerializeField] private int _offset;
        [SerializeField] private float _target, _threshold;
        [SerializeField] private UnityEvent<bool> _onValueChanged;

        public bool IsActive => _inRange;

        private float2 _rectSize;
        private Slider _slider;
        private bool _inRange;

        private void Awake() => _slider = GetComponent<Slider>();
        private void Start() { _rectSize = _direction.GetOrientation() * (float2)_areaRect.sizeDelta; SetTargetSize(); }
        private void OnEnable() { _slider.onValueChanged.AddListener(Perfome); _group?.RegisterToggle(this); }
        private void OnDisable() { _slider.onValueChanged.RemoveAllListeners(); _group?.UnregisterToggle(this); }

        public void SetRandomValue() { SetRandomTarget(); _slider.value = Random.Range(_slider.minValue, _slider.maxValue); }
        public void SetRandomTarget() { _target = Random.Range(_slider.minValue, _slider.maxValue); SetTargetSize(); }
        private void SetTargetSize()
        {
            if (!_targetRect) return;

            float2 area = _targetRect.sizeDelta;
            float2 position = (Vector2)_targetRect.localPosition;

            bool isHorizontal = _direction == Orientation.Horizontal;
            if (isHorizontal) area.x = _rectSize.x * _threshold; else area.y = _rectSize.y * _threshold;
            if (isHorizontal) position.x = _rectSize.x * _target - (area.x * 0.5f); else position.y = _rectSize.y * _target - (area.y * 0.5f);

            _targetRect.sizeDelta = area;
            _targetRect.localPosition = (Vector2)position;
        }

        private void Perfome(float value)
        {
            float size = _direction == Orientation.Horizontal ? _rectSize.x : _rectSize.y;
            float range = _slider.maxValue - _slider.minValue;
            float offset = (_offset / size) * range;

            float min = math.clamp(_target - offset - (_threshold * range), _slider.minValue, _slider.maxValue);
            float max = math.clamp(_target + offset + (_threshold * range), _slider.minValue, _slider.maxValue);

            _inRange = value >= min && value <= max;
            _onValueChanged.Invoke(_inRange);
            _group?.UpdateToggleStatus();
        }
    }
}