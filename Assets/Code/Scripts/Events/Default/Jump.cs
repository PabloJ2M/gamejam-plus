using UnityEngine;
using UnityEngine.Splines;

namespace Gameplay.Events
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Jump : MonoBehaviour
    {
        [SerializeField] private SplineContainer _spline;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _speed;
        [SerializeField, Range(0, 1)] private float _threshold;

        public float Velocity { set => _velocity = value; }

        private Transform _transform;
        private float _time, _velocity = 1;
        private bool _isJumping;

        private void Awake() => _transform = transform;
        private void ChangeState(bool value) { _isJumping = value; _time = 0; }
        public void AddForce() { if (!_isJumping || _time + _threshold >= 1) ChangeState(true); }

        private void Update()
        {
            if (!_isJumping) return;

            float time = _curve.Evaluate(_time);
            _transform.position = _spline.EvaluatePosition(time);
            _time = Mathf.MoveTowards(_time, 1, _speed * _velocity * Time.deltaTime);

            if (_time >= 1) { ChangeState(false); _transform.position = _spline.EvaluatePosition(0); }
        }
    }
}