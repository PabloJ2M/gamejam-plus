using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem
{
    public abstract class TouchBehaviour : InteractionBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        protected bool _isOverElement;
        protected bool _isSelected;

        protected virtual void Start() => _inputs.UI.Click.performed += OnSelectStatus;
        protected virtual void OnDestroy() => _inputs.UI.Click.performed -= OnSelectStatus;

        protected void OnSelectStatus(InputAction.CallbackContext ctx)
        {
            bool isPressed = ctx.control.IsPressed();
            if ((isPressed && !_isOverElement) || (!isPressed && !_isSelected)) return;

            if (isPressed) OnSelect();
            else OnDiselect();

            _isSelected = isPressed;
        }

        protected abstract void OnSelect();
        protected abstract void OnDiselect();

        public void OnPointerEnter(PointerEventData eventData) => _isOverElement = true;
        public void OnPointerExit(PointerEventData eventData) => _isOverElement = false;
    }
}