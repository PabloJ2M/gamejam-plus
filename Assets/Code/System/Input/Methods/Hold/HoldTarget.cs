using UnityEngine;
using UnityEngine.Events;

namespace UI.Inputs
{
    public class HoldTarget : MonoBehaviour
    {
        //[SerializeField] private ScreenPress _progressBar;
        [SerializeField] private RectTransform _target;
        [SerializeField] private float _padding;

        [SerializeField, Range(0, 1)] private float _speed;
        [SerializeField] private Vector2 _range;
        [SerializeField] private UnityEvent _onSuccess, _onFailure;

        private void Start()
        {
            //TargetMovement(_progressBar.Area() + _padding);
            TargetScale(Random.Range(_range.x, _range.y));
        }
        public void CompareResult()
        {
            //float area = _progressBar.Area() + _padding;
            //float target = _target.anchoredPosition.y + (area * 0.5f);
            //float range = _target.rect.height * 0.5f;

            //float value = _progressBar.Target();
            //if (value > target - range && value < target + range) _onSuccess.Invoke();
            //else _onFailure.Invoke();
            Start();
        }

        private void TargetMovement(float area)
        {
            float areaMovement = area - _target.rect.height;

            //Vector2 limit = areaMovement * 0.5f * _progressBar.Direction;
            //_target.localPosition = Vector2.Lerp(-limit, limit, Random.value);
        }
        private void TargetScale(float value)
        {
            _target.sizeDelta = new Vector2(_target.rect.width, value);
        }
    }
}