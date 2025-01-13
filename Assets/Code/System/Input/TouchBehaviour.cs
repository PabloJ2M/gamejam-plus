using UnityEngine.EventSystems;
//using Phase = UnityEngine.TouchPhase;

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
            else OnDeselect();

            _isSelected = isPressed;
        }
        //protected virtual void Update()
        //{
        //    if (Input.touchCount == 0) return;

        //    Touch touch = Input.GetTouch(0);
        //    bool isElement = IsPointerOverObject(gameObject); print(isElement);
        //    if (touch.phase == Phase.Began && isElement) { OnSelect(); _isOverElement = _isSelected = true; }
        //    else if ((touch.phase == Phase.Ended || touch.phase == Phase.Canceled) && _isSelected) { OnDeselect(); _isSelected = _isSelected = false; }
        //}

        protected abstract void OnSelect();
        protected abstract void OnDeselect();

        public void ForceDisable() { _isOverElement = _isSelected = false; }
        public void OnPointerEnter(PointerEventData eventData) => _isOverElement = true;
        public void OnPointerExit(PointerEventData eventData) => _isOverElement = false;
    }
}