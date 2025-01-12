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
        public void Save()
        {
            string data = JsonConvert.SerializeObject(_container); Debug.Log(data);
            if (!string.IsNullOrEmpty(data)) PlayerPrefs.SetString(_savePath, data);
        }
        public void Load()
        {
            if (!PlayerPrefs.HasKey(_savePath)) return;
            string data = PlayerPrefs.GetString(_savePath); Debug.Log(data);
            if (!string.IsNullOrEmpty(data)) _container = JsonConvert.DeserializeObject(data) as SlotGrid;
        }
    }
}