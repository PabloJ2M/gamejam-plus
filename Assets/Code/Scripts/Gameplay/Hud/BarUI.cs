using Unity.Mathematics;
using UnityEngine.Events;

namespace UnityEngine.UI
{
    public abstract class BarUI : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField, Range(0, 10)] private float _default, _min, _max;

        [SerializeField] private UnityEvent _onBarEmpty;

        protected float _current, _maxValue;
        protected bool _isLocked;

        protected virtual void Start() { _current = _maxValue = _default; UpdateUI(); }
        public virtual void IncrementValue(float amount) => _current += amount;
        public virtual void ReduceValue(float amount) => _current -= amount;
        public virtual void FillValue() => _current = _maxValue;
        public virtual void ResetValue() { Start(); _isLocked = false; }

        public void ReduceLimit(float amount) => _maxValue = math.clamp(_maxValue - amount, _min, _max);
        public void IncrementLimit(float amount) => _maxValue = math.clamp(_maxValue + amount, _min, _max);

        protected void UpdateUI()
        {
            _current = math.clamp(_current, 0, _max);
            _image.fillAmount = _current / _maxValue;

            if (_current != 0 || _isLocked) return;
            _onBarEmpty.Invoke();
            _isLocked = true;
        }
    }
}