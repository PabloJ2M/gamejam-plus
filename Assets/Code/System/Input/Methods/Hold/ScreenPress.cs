using UnityEngine.UI;
using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class ScreenPress : TouchBehaviour
    {
        [SerializeField] private Sprite _normal, _pressed;
        [SerializeField, Range(0, 1)] private float _speed;
        [SerializeField] private UnityEvent _onRelease;

        private Image _image;
        private Slider _slider;

        protected override void Awake() { base.Awake(); _image = GetComponent<Image>(); _slider = GetComponent<Slider>(); }
        protected override void OnSelect()
        {
            _slider.SetValueWithoutNotify(0);
            _image.sprite = _pressed;
        }
        protected override void OnDiselect()
        {
            _image.sprite = _normal;
            _slider.value -= 0.01f;
            _onRelease.Invoke();
        }

        private void FixedUpdate()
        {
            if (!_isSelected) return;
            _slider.SetValueWithoutNotify(_slider.value + _speed * Time.fixedDeltaTime);
        }
    }
}