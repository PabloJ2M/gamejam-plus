using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Splines;
using Unity.Mathematics;
using Random = UnityEngine.Random;

namespace UI.Effects
{
    public class TrackSplineRandomizer : ItemBehaviour
    {
        [SerializeField] private SplineContainer _spline;
        [SerializeField, Range(0, 1)] private float _threshold;

        private RectTransform _area;

        protected override void Awake() { base.Awake(); _area = _transform as RectTransform; }
        private void Start() => CalculateSpline();

        [ContextMenu("Randomize Spline")] public void CalculateSpline()
        {
            if (!_area) Awake();
            if (_spline.Spline.Count < 3) return;

            float2 min = _area.rect.min;
            float2 max = _area.rect.max;

            float3 top = new(Random.Range(min.x, max.x), max.y, 0);
            float3 bottom = new(Random.Range(min.x, max.x), min.y, 0);

            float margin = math.abs(max.y - min.y) * _threshold;
            float3 middle = new(Random.Range(min.x, max.x), Random.Range(min.y + margin, max.y - margin), 0);

            _spline.Spline[0] = new(top);
            _spline.Spline[1] = new(middle);
            _spline.Spline[2] = new(bottom);
        }
    }
}