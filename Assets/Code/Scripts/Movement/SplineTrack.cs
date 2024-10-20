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
        private float3 position, tangent, up;

        private void Awake() => _spline = GetComponent<SplineContainer>();
        private void Update()
        {
            float t = Mathf.PingPong(Time.time * _speed, 1);
            _spline.Evaluate(t, out position, out tangent, out up);
            _object.position = position;
        }
    }
}