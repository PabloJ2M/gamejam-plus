using UnityEngine.InventorySystem;

namespace UnityEngine.UI
{
    public class InventoryUI : UI_Builder
    {
        [SerializeField] private SlotStorage _storage;

        public SlotStorage Storage => _storage;

        private void Start() => _storage.Load();
        public void RefreshUI() { OnDisplay(); _storage.Save(); }
        
        protected override void OnDisplay()
        {
            var slots = _storage.Container.slots;

            foreach (var slot in slots) { var item = Pool.Get() as InventoryUI_Entry; item.Setup(slot.Value); }
            OnRemoveExceed(slots.Count);
        }
    }
}