using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;
using Random = UnityEngine.Random;

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
        private void Start() => _random = Random.value;
        private void Update()
        {
            float t = Mathf.PingPong((Time.time + _random) * _speed, 1);
            _spline.Evaluate(t, out _position, out _tangent, out _up);
            _object.position = _position;
        }

        [ContextMenu("Randomize")] public void Randomize()
        {
            if (!_spline) Awake();

            var spline = _spline.Spline;
            if (spline.Count == 0) return;

            for (int i = 0; i < spline.Count; i++)
            {
                var point = spline[i];
                float target = (Random.value * 2) - 1;
                point.Position = new(target, point.Position.yz);
                //point.TangentIn = new(-1, 0, 0);
                //point.TangentOut = new(1, 0, 0);
                spline[i] = point;
            }
        }
    }
}