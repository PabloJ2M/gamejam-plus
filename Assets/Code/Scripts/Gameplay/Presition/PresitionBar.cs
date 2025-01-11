using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Presition
{
    public class PresitionBar : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private bool _loop, _backwards;
        [SerializeField] private UnityEvent<float> _onValueChanged;

        private float _time;

        private void FixedUpdate()
        {
            _time += (_backwards ? _speed : -_speed) * Time.fixedDeltaTime;
            _onValueChanged.Invoke(_loop ? Mathf.PingPong(_time, 1) : _time % 1f);
        }
    }
}