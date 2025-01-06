using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Inputs
{
    public class LoadProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private RectTransform _target;
        [SerializeField, Range(0, 1)] private float _speed;

        private bool _isHold;

        private void FixedUpdate()
        {
            float velocity = _speed * Time.fixedDeltaTime;
            _image.fillAmount += _isHold ? velocity : 0;

            if (!_target) return;
            _target.anchoredPosition = (1 - _image.fillAmount) * _image.rectTransform.rect.height * Vector2.down;
        }

        public void Hold() => _isHold = true;
        public void Hold(BaseEventData data) => Hold();
        public void Release() => _isHold = false;
        public void Release(BaseEventData data) => Release();
        public void ResetBar() => _image.fillAmount = 0;

        public Vector2 Direction => _image.GetDirection();
        public float Target() => (_image.fillAmount * _image.rectTransform.rect.height) + _target.rect.height;
        public float Area() => _image.rectTransform.rect.height + _target.rect.height;
    }
}