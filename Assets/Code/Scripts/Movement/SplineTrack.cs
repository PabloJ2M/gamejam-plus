using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace Controller
{
    [RequireComponent(typeof(SplineContainer))]
    public class SplineTrack : MonoBehaviour
    {
        [SerializeField] private Transform _object;
        [SerializeField, Range(0, 1)] private float _speed;
        
        private SplineContainer _spline;
        private float3 _position, _tangent, _up;
        private float _random;

        private void Awake() => _spline = GetComponent<SplineContainer>();
        private void Start() => _random = UnityEngine.Random.value;
        private void Update()
        {
            float t = Mathf.PingPong((Time.time + _random) * _speed, 1);
            _spline.Evaluate(t, out _position, out _tangent, out _up);
            _object.position = _position;
        }
    }
}