using System;

namespace UnityEngine.InventorySystem
{
    [CreateAssetMenu(fileName = "storage", menuName = "system/inventory/storage", order = 1)]
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
            string data = JsonUtility.ToJson(_container); Debug.Log(data);
            if (!string.IsNullOrEmpty(data)) PlayerPrefs.SetString(_savePath, data);
        }
        public void Load()
        {
            if (!PlayerPrefs.HasKey(_savePath)) return;
            string data = PlayerPrefs.GetString(_savePath); Debug.Log(data);
            
            if (string.IsNullOrEmpty(data)) return;
            _container = JsonUtility.FromJson<SlotGrid>(data);
        }
    }
}