using UnityEngine.InventorySystem;
using Player.Data;

namespace UnityEngine.UI
{
    public class StoreUI : UI_Builder
    {
        [SerializeField] private InventoryUI _inventory;
        [SerializeField] private Database _database;

        protected override void OnDisplay()
        {
            ClearItems();
            var items = _database.Items;
            for (int i = 0; i < items.Count; i++)
            {
                var item = Pool.Get() as StoreUI_Entry;
                item.Setup(items[i]);
            }
        }

        public void BuyItem(Item item)
        {
            Vector3 data = ResourceManager.GetResource();
            if (data.x < item.Hearts || data.y < item.Coins) return;

            ResourceManager.RemoveResource(ResourceType.Hearts, item.Hearts);
            ResourceManager.RemoveResource(ResourceType.Coins, item.Coins);

            _inventory.Storage.Container?.Add(item, 1);
            _inventory?.RefreshUI();
        }
    }
}