using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Splines;

namespace Gameplay.Events
{
    [RequireComponent(typeof(SplineContainer))]
    public class TrackSpline : MonoBehaviour
    {
        [SerializeField] private Transform _object;
        [SerializeField, Range(0, 5)] private float _duration;
        
        private SplineContainer _spline;
        private bool _isPlaying;
        private float _init, _time;

        private void Awake() => _spline = GetComponent<SplineContainer>();
        private void Start() { SetInitTime(); OnStop(); }

        [ContextMenu("Reset")] public void SetInitTime() => _init = Random.value;
        [ContextMenu("Start")] public void OnStart() { _isPlaying = true; _time = _init; }
        [ContextMenu("Stop")] public async void OnStop()
        {
            await Task.Yield();
            _isPlaying = false;
            _object.position = _spline.EvaluatePosition(_init);
        }

        private void Update()
        {
            if (!_isPlaying) return;
            _time += Time.deltaTime;

            float t = Mathf.PingPong(_time, 1);
            _object.position = _spline.EvaluatePosition(t);
        }
    }
}