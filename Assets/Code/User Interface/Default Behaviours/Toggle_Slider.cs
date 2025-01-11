using UnityEngine.Events;
using Unity.Mathematics;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Slider))]
    public class Toggle_Slider : MonoBehaviour, IToggle
    {
        [SerializeField] private RectTransform _container, _visualTarget;
        [SerializeField] private ActivationGroup _group;

        [SerializeField] private Orientation _orientation;

        [SerializeField] private RectOffset _padding;
        [SerializeField] private float _target, _threshold;
        [SerializeField] private UnityEvent<bool> _onValueChanged;
        
        public bool IsActive => _inRange;

        private bool _isHorizontal, _inRange;
        private Slider _slider;
        private float _size;

        private void Awake() { _slider = GetComponent<Slider>(); _isHorizontal = _orientation.IsDirection(Orientation.Horizontal); }
        private void Start() { _size = _isHorizontal ? _container.rect.width : _container.rect.height; SetTargetSize(); }
        private void OnEnable() { _slider.onValueChanged.AddListener(Perfome); _group?.RegisterToggle(this); }
        private void OnDisable() { _slider.onValueChanged.RemoveAllListeners(); _group?.UnregisterToggle(this); }
        private float RandomValue() => Random.Range(_slider.minValue + _threshold, _slider.maxValue - _threshold);

        public void ForceUpdate() => _slider.value -= 0.01f;
        public void SetRandomValue() { SetRandomTarget(); _slider.value = RandomValue(); }
        public void SetRandomTarget() { _target = RandomValue(); SetTargetSize(); }
        public void SetRandomThreshold() => _threshold = Random.Range(0.1f, 0.3f);

        private void SetTargetSize()
        {
            if (!_visualTarget) return;

            float rectSize = _size * _threshold;
            float position = _size * _target;
            position -= _size * 0.5f;

            float2 rect = _visualTarget.sizeDelta;
            _visualTarget.sizeDelta = new(_isHorizontal ? rectSize : rect.x, _isHorizontal ? rect.y : rectSize);
            _visualTarget.localPosition = new(_isHorizontal ? position : 0, _isHorizontal ? 0 : position);
        }
        private void Perfome(float value)
        {
            float2 padding = _isHorizontal ? new(_padding.left, _padding.right) : new(_padding.bottom, _padding.top);
            padding /= _size;
            padding *= 0.5f;

            float target = _target + padding.x - padding.y;
            float min = target - _threshold * 0.5f;
            float max = target + _threshold * 0.5f;

            _inRange = value >= min && value <= max;
            _onValueChanged.Invoke(_inRange);
            _group?.UpdateToggleStatus();
        }
    }
}