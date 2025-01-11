using UnityEngine;
using UnityEngine.Events;

namespace Controller.Movement
{
    public class TrackLineRenderer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _render;
        [SerializeField, Range(0, 5)] private float _speed;
        [SerializeField] private UnityEvent _onStartTrack, _onCompleteTrack;

        private Transform _transform;
        private bool _isEnebled;
        private float _time;

        private void Awake() => _transform = transform;
        public void StartTrack() { if (_render.positionCount <= 1) return; _time = 0; _isEnebled = true; _onStartTrack.Invoke(); }
        public void ResetTrack() => _transform.localPosition = Vector3.zero;
        public void StopTrack() { _isEnebled = false; _onCompleteTrack.Invoke(); }
        
        private void FixedUpdate()
        {
            if (!_isEnebled) return;
            _time = Mathf.Clamp01(_time + _speed * Time.fixedDeltaTime);

            int count = _render.positionCount - 1;
            float progress = _time * count;

            int index = Mathf.FloorToInt(progress);
            float segment = progress - index;

            index = Mathf.Clamp(index, 0, count - 1);
            Vector2 initPos = _render.GetPosition(index);
            Vector2 endPos = _render.GetPosition(index + 1);

            _transform.position = Vector2.Lerp(initPos, endPos, segment);

            //complete track event
            if ((_transform.position - _render.GetPosition(count)).magnitude > 0.1f) return;
            StopTrack();
        }
    }
}