using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public abstract class DragBehaviour : TouchBehaviour
    {
        protected bool _isDragging;

        protected override void Start() { base.Start(); _inputs.UI.Point.performed += OnPointerUpdate; }
        protected override void OnDestroy() { base.OnDestroy(); _inputs.UI.Point.performed -= OnPointerUpdate; }

        protected void OnPointerUpdate(InputAction.CallbackContext ctx)
        {
            if (!_isDragging) return;

            float2 input = ctx.ReadValue<Vector2>();
            input = math.clamp(input, mathf.zero, new float2(Screen.width, Screen.height));
            OnUpdateSelection(input);
        }

        protected void ForceUpdate() => OnUpdateSelection(_inputs.UI.Point.ReadValue<Vector2>());
        protected abstract void OnUpdateSelection(Vector2 screenPosition);

        protected override void OnSelect() => _isDragging = true;
        protected override void OnDeselect() => _isDragging = false;
    }
}