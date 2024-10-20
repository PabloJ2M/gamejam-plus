using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    }
}