using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public class DeltaBehaviour : DragBehaviour
    {
        protected override void Start()
        {
            _inputs.UI.Click.performed += OnSelectStatus;
            _inputs.UI.Delta.performed += OnPointerUpdate;
        }
        protected override void OnDestroy()
        {
            _inputs.UI.Click.performed -= OnSelectStatus;
            _inputs.UI.Delta.performed -= OnPointerUpdate;
        }

        protected override void OnUpdateSelection(float2 delta) { }
    }
}