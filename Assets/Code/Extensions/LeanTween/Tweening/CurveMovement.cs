using UnityEngine;
using Unity.Mathematics;

namespace UI.Effects
{
    public class CurveMovement : MonoBehaviour
    {
        [SerializeField] private RectTransform _element;
        [SerializeField] private float _duration;
        [SerializeField] private bool _invertDirection;

        private RectTransform _area;
        private bool _isEnabled;
        private float _time;

        private void Awake() => _area = GetComponent<RectTransform>();
        private void Update()
        {
            if (!_isEnabled) return;
            _time += Time.deltaTime;
            float t = Mathf.PingPong(_time / _duration, 1);
            _element.localPosition = CalculatePosition(t);
        }
        private Vector2 CalculatePosition(float t)
        {
            float heigh = _area.rect.height * 0.5f;
            float width = _area.rect.width;
            float2 end = new(0, -heigh);
            float2 start = new(0, heigh);
            float2 control = new(_invertDirection ? -width : width, 0);

            return math.pow(1 - t, 2) * start +
                   2 * (1 - t) * t * control +
                   math.pow(t, 2) * end;
        }

        [ContextMenu("Start")] public void OnStart()
        {
            _isEnabled = true;
            _time = 0;
        }
        [ContextMenu("Stop")] public void OnStop()
        {
            _element.localPosition = CalculatePosition(0);
            _isEnabled = false;
        }
    }
}