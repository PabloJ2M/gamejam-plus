using UnityEngine.UI;
using UnityEngine.Events;
using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public class ScreenShake : DeltaBehaviour
    {
        [SerializeField] private AdvancedOrientation _orientation;
        [SerializeField, Range(0, 1)] private float _threshold;
        [SerializeField, Range(0, 10)] private int _amount;

        [SerializeField] private UnityEvent _onSuccess;

        private bool _inverseDirection;
        private int _current;

        public AdvancedOrientation Orientation { set => _orientation = value; }

        protected override void OnUpdateSelection(float2 delta)
        {
            float2 direction = _orientation.GetOrientation();
            if (math.length(delta) < 5) return;

            float value = math.dot(direction, math.normalize(delta));
            if (value > _threshold && _inverseDirection) MatchDirection(true);
            if (value < -_threshold && !_inverseDirection) MatchDirection(false);
        }
        private void MatchDirection(bool isForward)
        {
            _inverseDirection = !isForward;
            _current++;

            if (_current < _amount) return;
            _onSuccess.Invoke();
            _current = 0;
        }
    }
}