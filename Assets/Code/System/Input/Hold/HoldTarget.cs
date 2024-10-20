using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UI.Inputs
{
    public class HoldTarget : MonoBehaviour
    {
        [SerializeField] private RectTransform _container, _target;
        [SerializeField] private Image _progress;
        [SerializeField] private float _padding;

        [SerializeField, Range(0, 1)] private float _speed;
        [SerializeField] private Vector2 _range;
        [SerializeField] private UnityEvent _onSuccess;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(Random.Range(2, 4));
            LeanTween.cancel(gameObject);
            LeanTween.value(gameObject, _target.rect.height, Random.Range(_range.x, _range.y), 0.5f).setOnUpdate(TargetScale);
            StartCoroutine(Start());
        }

        private void Update()
        {
            float area = _container.rect.height + _padding;
            
            TargetMovement(area);

            float fill = (_progress.fillAmount * _container.rect.height) + 50;
            float target = _target.anchoredPosition.y + area * 0.5f;
            float range = _target.rect.height * 0.5f;

            if (fill > target - range && fill < target + range) { _onSuccess.Invoke(); }
        }

        private void TargetMovement(float area)
        {
            float areaMovement = area - _target.rect.height;
            float t = Mathf.PingPong(Time.time * _speed, 1);

            Vector2 limit = areaMovement * 0.5f * _progress.GetDirection();
            _target.localPosition = Vector2.Lerp(-limit, limit, t);
        }
        private void TargetScale(float value)
        {
            _target.sizeDelta = new Vector2(_target.rect.width, value);
        }
    }
}