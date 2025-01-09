using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class ScreenTap : TouchBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private bool _loop, _backwards;
        [SerializeField] private UnityEvent<float> _onValueChanged;
        [SerializeField] private UnityEvent _onPress;

        private float _time;

        protected override void OnSelect() => _onPress.Invoke();
        protected override void OnDeselect() { }

        private void FixedUpdate()
        {
            _time += (_backwards ? _speed : -_speed) * Time.fixedDeltaTime;
            _onValueChanged.Invoke(_loop ? Mathf.PingPong(_time, 1) : _time % 1f);
        }
    }
}