using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
    public class InventoryUI_Handler : MonoBehaviour, IDragHandler, IDropHandler
    {
        private InventoryUI _inventory;

        private void Awake() => _inventory = GetComponentInParent<InventoryUI>();

        public void OnDrag(PointerEventData eventData) { }
        public void OnDrop(PointerEventData eventData)
        {
            //_inventory.Storage.Container.MoveIndex(0, 1);
            //_inventory.RefreshUI();
        }
    }
}