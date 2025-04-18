using System;
using System.Linq;
using System.Collections.Generic;

namespace UnityEngine.InventorySystem
{
    [Serializable] public class SlotGrid
    {
        /// <summary>int: Slot Index, Slot: Slot Value</summary>
        public SerializableDictionary<int, Slot> slots = new();

        public void Add(Item item, int amount)
        {
            do {
                int index = FindAvailableSlot(item); if (index < 0) return;
                if (!slots.ContainsKey(index)) slots.Add(index, new(item));

                int space = item.Max - slots[index].Amount;
                slots[index].Add(amount < space ? amount : space);
                amount -= space;
            }
            while (amount > 0);
        }
        public void Remove(Item item)
        {
            var search = Find(item, out bool found, x => !x.Value.IsEmpty());
            if (!found) return;

            search.Value.Remove(1);
            if (search.Value.IsEmpty()) slots.Remove(search.Key);
        }
        public void MoveIndex(int from, int to)
        {
            if (slots.ContainsKey(to)) return;
            slots[to] = slots[from];
            slots.Remove(from);
        }

        private KeyValuePair<int, Slot> Find(Item item, out bool found, Func<KeyValuePair<int, Slot>, bool> first)
        {
            var search = slots.OrderBy(key => key.Key).Where(x => x.Value.ID == item.ID).FirstOrDefault(first);
            found = !search.Equals(default(KeyValuePair<int, Slot>));
            return search;
        }
        private int FindAvailableSlot(Item item)
        {
            var search = Find(item, out bool found, x => x.Value.HasSpace(item.Max));
            if (found) return search.Key;

            for (int i = 0; i < slots.Count; i++) { if (!slots.ContainsKey(i)) return i; }
            return slots.Count;
        }
    }
}