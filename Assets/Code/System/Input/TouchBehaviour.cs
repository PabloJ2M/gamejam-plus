using UnityEngine.EventSystems;
using Phase = UnityEngine.TouchPhase;

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

            if (Input.touchCount != 0) {
                _isOverElement = Input.touches[0].phase == Phase.Began ? IsPointerOverObject(gameObject) : false;
            }
            
            if ((isPressed && !_isOverElement) || (!isPressed && !_isSelected)) return;
            if (isPressed) OnSelect();
            else OnDeselect();

            _isSelected = isPressed;
        }

        protected abstract void OnSelect();
        protected abstract void OnDeselect();

        public void ForceDisable() { _isOverElement = _isSelected = false; }

        public void OnPointerEnter(PointerEventData eventData) => _isOverElement = true;
        public void OnPointerExit(PointerEventData eventData) => _isOverElement = false;
    }
}