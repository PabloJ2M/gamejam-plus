using System;
using System.Collections.Generic;

namespace UnityEngine.DecorationSystem
{
    [Serializable] public struct Deco
    {
        public ItemDec item;
        public Vector2Int position;

        public Deco(ItemDec item, Vector2Int position)
        {
            this.item = item;
            this.position = position;
        }
    }

    [Serializable] public class DecorationContainer
    {
        [SerializeField] private List<Deco> items = new();
        public List<Deco> Items => items;
    }

    [CreateAssetMenu(fileName = "decoration storage", menuName = "System/Decoration/Storage", order = 0)]
    public class DecorationStorage : ScriptableObject
    {
        [SerializeField] private DecorationContainer container;

        public DecorationContainer Container => container;
        private const string _data = "Decoration";

        public void AddItem(Deco item) => container.Items.Add(item);
        public void RemoveItem(Deco item) => container.Items.Remove(item);
        public bool CanPlaceItem(Vector2Int position) => !container.Items.Exists(x => x.position == position);

        public void Save()
        {
            PlayerPrefs.SetString(_data, JsonUtility.ToJson(container));
            PlayerPrefs.Save();
        }
        public void Load()
        {
            string json = PlayerPrefs.GetString(_data);
            if (string.IsNullOrEmpty(json)) { container.Items.Clear(); return; }
            container = JsonUtility.FromJson<DecorationContainer>(json);
        }
    }
}