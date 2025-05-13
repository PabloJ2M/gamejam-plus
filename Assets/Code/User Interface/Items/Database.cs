using System.Collections.Generic;

namespace UnityEngine.InventorySystem
{
    [CreateAssetMenu(fileName = "database", menuName = "system/inventory/database", order = 0)]
    public class Database : ScriptableObject
    {
        [SerializeField] private List<Item> _items = new();

        public List<Item> Items => _items;
        public void SetItems(List<Item> items) => _items = items;

        public Item GetItemByIndex(int index) => _items.Find(x => x.ID == index);
    }
}