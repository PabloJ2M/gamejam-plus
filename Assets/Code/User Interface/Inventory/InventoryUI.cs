using UnityEngine.InventorySystem;

namespace UnityEngine.UI
{
    public class InventoryUI : UI_Builder
    {
        [SerializeField] private SlotStorage _storage;

        public SlotStorage Storage => _storage;

        private void Start() { _storage.Load(); RefreshUI(); }
        private void OnDisable() => _storage.Save();

        protected override void OnEnable() { }
        public void RefreshUI() => OnDisplay();
        
        protected override void OnDisplay()
        {
            ClearItems();
            var slots = _storage.Container.slots;
            foreach (var slot in slots) { var item = Pool.Get() as InventoryUI_Entry; item.Setup(slot.Value); }
        }
    }
}