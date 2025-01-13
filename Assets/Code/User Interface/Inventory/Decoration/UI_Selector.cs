using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

namespace UnityEngine.DecorationSystem
{
    [RequireComponent(typeof(InventoryUI_Entry))]
    public class UI_Selector : TouchBehaviour, IPointerEnterHandler
    {
        private UI_Handler _handler;
        private InventoryUI_Entry _entry;

        protected override void Awake()
        {
            base.Awake();
            _entry = GetComponent<InventoryUI_Entry>();
            _handler = GetComponentInParent<UI_Handler>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_entry.CurrentItem is not ItemDec item) return;
            _handler.OnPreview(item);
        }
        protected override void OnSelect()
        {
            if (_entry.CurrentItem is not ItemDec item) return;
            _handler?.OnDrag(item);
            _handler.OnPreview(item);
        }
        protected override void OnDeselect() => _handler?.OnDrop(IsPointerOverUI());
    }
}