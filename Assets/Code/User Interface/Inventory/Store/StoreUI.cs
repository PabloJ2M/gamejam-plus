using UnityEngine.InventorySystem;

namespace UnityEngine.UI
{
    public class StoreUI : UI_Builder
    {
        [SerializeField] private InventoryUI _inventory;
        [SerializeField] private Database _database;

        protected override void OnDisplay()
        {
            var items = _database.Items;

            for (int i = 0; i < items.Count; i++) { var item = Pool.Get() as StoreUI_Entry; item.Setup(items[i]); }
            OnRemoveExceed(items.Count);
        }

        public void BuyItem(Item item)
        {
            //permitir compra si tiene suficiente dinero

            _inventory.Storage.Container?.Add(item, 1);
            _inventory?.RefreshUI();
        }
    }
}