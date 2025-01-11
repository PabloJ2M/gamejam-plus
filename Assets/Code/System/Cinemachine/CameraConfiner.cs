using UnityEngine;
using Unity.Mathematics;

namespace Unity.Cinemachine
{
    [RequireComponent(typeof(CinemachineConfiner2D))]
    public class CameraConfiner : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        private CinemachineConfiner2D _confiner2D;
        private Vector2 _size;

        private void Awake() => _confiner2D = GetComponent<CinemachineConfiner2D>();
        private void Start() => _size = _camera.ViewportToWorldPoint(Vector2.one) - _camera.transform.position;
        private void LateUpdate()
        {
            Vector3 position = transform.position;
            Bounds area = _confiner2D.BoundingShape2D.bounds;

            position.x = math.clamp(position.x, area.min.x + _size.x, area.max.x - _size.x);
            position.y = math.clamp(position.y, area.min.y + _size.y, area.max.y - _size.y);
            
            transform.position = position;
        }
    }
}