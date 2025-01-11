using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
public class SineWave : MonoBehaviour
{
    [SerializeField] private RectTransform _area;
    [SerializeField, Range(0, 1)] private float _frequency, _noise;
    [SerializeField] private float _speed;
    [SerializeField] private int _margin;

    [SerializeField] private UnityEvent _onComplete;

    private LineRenderer _render;
    private float _width, _height, _offsetX;
    private bool _isCompleted;

    private void Awake() => _render = GetComponent<LineRenderer>();
    public void SetNoise(float value)
    {
        _noise = 1 - value;

        if (_noise != 0 && _isCompleted) _isCompleted = false;
        else if (_noise == 0 && !_isCompleted)
        {
            _isCompleted = true;
            _onComplete.Invoke();
        }
    }

    private void Start()
    {
        _width = _area.rect.width - _margin * 2;
        _height = _area.rect.height * 0.5f;
        _offsetX = -_area.rect.width * 0.5f + _margin;
    }
    private void Update()
    {
        int length = _render.positionCount - 1;
        float time = Time.timeSinceLevelLoad;

        for (int i = 0; i <= length; i++)
        {
            float t = (float)i / length;
            float x = math.lerp(0, _width, t) + _offsetX;

            float value = _height * math.sin(t * _frequency * math.PI * 2 + _speed * time);
            float noise = _height * 2 * Mathf.PerlinNoise(t * 10f, _speed * time) - _height;
            float y = math.lerp(value, noise, _noise);

            _render.SetPosition(i, new float3(x, y, 0));
        }
    }
}