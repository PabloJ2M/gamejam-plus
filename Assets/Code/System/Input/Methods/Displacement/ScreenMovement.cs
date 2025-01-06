using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class ScreenMovement : DeltaBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private bool _inverse;
        [SerializeField] private UnityEvent<Vector3> _onValueChanged;

        protected override void OnUpdateSelection(Vector2 delta)
        {
            Vector2 scroll = _inverse ? -delta : delta;
            _onValueChanged.Invoke(_speed * scroll * Time.deltaTime);
        }
    }
}