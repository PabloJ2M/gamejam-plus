using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Storage", menuName = "Inventory/Storage", order = 0)]
    public class ItemStorage : ScriptableObject
    {
        [SerializeField] private List<Item> _items = new();

        public void CheckItemsID()
        {
            //clear list from duplicated objects
            var uniqueList = new List<Item>();
            foreach (var item in _items) { if (!uniqueList.Contains(item)) uniqueList.Add(item); }
            _items = uniqueList;

            //compare objects id
            var groups = _items.GroupBy(x => x.ID);
            foreach (var item in groups)
            {
                if (item.Count() == 1) continue;
                Debug.LogWarning($"item has duplicated ID: {item.Key}", item.Last());
            }
        }
    }
}