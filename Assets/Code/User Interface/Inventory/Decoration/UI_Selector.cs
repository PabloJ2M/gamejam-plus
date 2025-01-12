using UnityEngine.UI;
using UnityEngine.InputSystem;

namespace UnityEngine.DecorationSystem
{
    [RequireComponent(typeof(InventoryUI_Entry))]
    public class UI_Selector : TouchBehaviour
    {
        private UI_Handler _handler;
        private InventoryUI_Entry _entry;

        protected override void Awake()
        {
            base.Awake();
            _entry = GetComponent<InventoryUI_Entry>();
            _handler = GetComponentInParent<UI_Handler>();
        }

        protected override void OnDeselect() => _handler?.OnDrop(IsPointerOverUI());
        protected override void OnSelect()
        {
            if (_entry.CurrentItem is not ItemDec item) return;
            _handler?.OnDrag(item);
        }
    }
}