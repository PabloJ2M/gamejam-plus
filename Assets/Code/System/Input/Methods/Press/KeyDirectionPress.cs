using UnityEngine.UI;
using UnityEngine.Events;
using Unity.Mathematics;

namespace UnityEngine.InputSystem
{
    public class KeyDirectionPress : ActionsBehaviour
    {
        [SerializeField] private Orientation _orientation;
        [SerializeField] private bool _isPositive;

        [SerializeField] private UnityEvent<bool> _onKeyPressed;

        private bool _isHorizontal;

        protected override void Awake() { base.Awake(); _isHorizontal = _orientation.IsDirection(Orientation.Horizontal); }
        private void Start() => _inputs.UI.Navigate.performed += OnPerforme;
        private void OnDestroy() => _inputs.UI.Navigate.performed -= OnPerforme;

        private void OnPerforme(InputAction.CallbackContext ctx)
        {
            float2 input = ctx.ReadValue<Vector2>();
            float value = _isHorizontal ? input.x : input.y;

            bool state = (value > 0 && _isPositive) || (value < 0 && !_isPositive);
            _onKeyPressed.Invoke(state);
        }
    }
}