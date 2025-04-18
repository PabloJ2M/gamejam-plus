using System;

namespace UnityEngine.InventorySystem
{
    [Serializable] public class Slot
    {
        [SerializeField] private int _id;
        [SerializeField] private int _amount;

        public int ID => _id;
        public int Amount => _amount;
        public Slot(Item item) { _id = item.ID; _amount = 0; }

        public void Add(int value) => _amount += value;
        public void Remove(int value) => _amount -= value;
        public bool HasSpace(int length) => length > _amount;
        public bool IsEmpty() => _amount <= 0;
    }
}