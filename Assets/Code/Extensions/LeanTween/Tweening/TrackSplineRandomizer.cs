using UnityEngine;
using UnityEngine.Splines;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace UI.Effects
{
    public class TrackSplineRandomizer : MonoBehaviour
    {
        [SerializeField] private SplineContainer _spline;
        [SerializeField, Range(0, 1)] private float _threshold;

        private RectTransform _area;

        private void Awake() => _area = GetComponent<RectTransform>();
        private void Start() => CalculateSpline();

        [ContextMenu("Randomize Spline")] public void CalculateSpline()
        {
            if (!_area) Awake();
            if (_spline.Spline.Count < 3) return;

            float2 min = _area.rect.min;
            float2 max = _area.rect.max;
            float3 worldMin = _area.TransformPoint(new(min.x, min.y, 0));
            float3 worldMax = _area.TransformPoint(new(max.x, max.y, 0));

            float3 top = new(Random.Range(worldMin.x, worldMax.x), worldMax.y, 0);
            float3 bottom = new(Random.Range(worldMin.x, worldMax.x), worldMin.y, 0);

            float margin = math.abs(worldMax.y - worldMin.y) * _threshold;
            float3 middle = new(Random.Range(worldMin.x, worldMax.x), Random.Range(worldMin.y + margin, worldMax.y - margin), 0);

            _spline.Spline[0] = new(top);
            _spline.Spline[1] = new(middle);
            _spline.Spline[2] = new(bottom);
        }
    }
}