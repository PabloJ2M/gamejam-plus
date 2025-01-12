using System;
using Newtonsoft.Json;

namespace UnityEngine.InventorySystem
{
    [CreateAssetMenu(fileName = "Storage", menuName = "System/Inventory/Storage", order = 1)]
    [Serializable] public class SlotStorage : ScriptableObject
    {
        [SerializeField] private Database _database;
        [SerializeField] private SlotGrid _container;
        private const string _savePath = "inventory";

        public Database Database => _database;
        public SlotGrid Container => _container;

        //-----change for better data saving-----
        public void Save() => PlayerPrefs.SetString(_savePath, JsonConvert.SerializeObject(_container));
        public void Load() => _container = JsonConvert.DeserializeObject(PlayerPrefs.GetString(_savePath)) as SlotGrid;
    }
}