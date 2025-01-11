using UnityEngine.Events;
using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public class ScreenPosition : DragBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private UnityEvent<Vector3> _onPositionChanged;

        protected override void OnUpdateSelection(Vector2 screenPosition)
        {
            float3 worldPosition = _camera.ScreenToWorldPoint(screenPosition);
            worldPosition.z = 0;

            _onPositionChanged.Invoke(worldPosition);
        }
    }
}