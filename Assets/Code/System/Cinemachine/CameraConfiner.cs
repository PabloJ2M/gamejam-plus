using UnityEngine;

namespace Unity.Cinemachine
{
    [RequireComponent(typeof(CinemachineConfiner2D))]
    public class CameraConfiner : MonoBehaviour
    {
        private CinemachineConfiner2D _confiner2D;

        private void Awake() => _confiner2D = GetComponent<CinemachineConfiner2D>();
        private void LateUpdate()
        {
            if (_confiner2D.BoundingShape2D == null) return;

            Vector3 position = transform.position;
            Vector2 confinedPosition = _confiner2D.BoundingShape2D.ClosestPoint(position);

            transform.position = new(confinedPosition.x, confinedPosition.y, position.z);
        }
    }
}