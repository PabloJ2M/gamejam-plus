using Unity.Mathematics;
using UnityEngine.Events;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Slider))]
    public class Toggle_Slider : MonoBehaviour, IToggle
    {
        [SerializeField] private ActivationGroup _group;
        [SerializeField] private RectTransform _area;
        [SerializeField] private Orientation _direction;
        [SerializeField] private int _offset;
        [SerializeField] private float _target, _threshold;
        [SerializeField] private UnityEvent<bool> _onValueChanged;

        public bool IsActive => _inRange;

        private float2 _rectSize;
        private Slider _slider;
        private bool _inRange;

        private void Awake() => _slider = GetComponent<Slider>();
        private void Start() => _rectSize = _direction.GetOrientation() * (float2)_area.sizeDelta;
        private void OnEnable()
        {
            _slider.onValueChanged.AddListener(Perfome);
            _group?.RegisterToggle(this);
        }
        private void OnDisable()
        {
            _slider.onValueChanged.RemoveAllListeners();
            _group?.UnregisterToggle(this);
        }

        public void SetRandomValue()
        {
            _target = Random.Range(_slider.minValue, _slider.maxValue);
            _slider.value = Random.Range(_slider.minValue, _slider.maxValue);
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