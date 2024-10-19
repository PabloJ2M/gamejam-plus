using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 1)]
    public class Item : ScriptableObject
    {
        [SerializeField] private int _id = -1;

        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;
        [SerializeField] private int _cost;

        public int ID => _id;

        public Sprite Icon => _icon;
        public string Name => _name;
        public int Cost => _cost;

        private void OnEnable() { if (_id < 0) GetRandomID(); }
        public void GetRandomID() => _id = Random.Range(0, 10000);
    }
}