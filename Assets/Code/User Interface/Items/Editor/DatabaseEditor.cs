using System.Linq;
using System.Collections.Generic;
using UnityEditor;

namespace UnityEngine.InventorySystem
{
    [CustomEditor(typeof(Database))]
    public class DatabaseEditor : Editor
    {
        public string _ruth = "Items";

        public override void OnInspectorGUI()
        {
            Database data = target as Database;

            GUILayout.Label("Path: Resources/~");
            _ruth = GUILayout.TextField(_ruth);

            if (GUILayout.Button("Get All Items in Ruth"))
            {
                //load item from resources
                data.SetItems(Resources.LoadAll(_ruth, typeof(Item)).Cast<Item>().OrderBy(x => x.Name).ToList());
            }

            if (GUILayout.Button("Check Items ID's"))
            {
                //clear list from duplicated objects
                var uniqueList = new List<Item>();
                foreach (var item in data.Items) { if (!uniqueList.Contains(item)) uniqueList.Add(item); }
                data.SetItems(uniqueList);

                //compare objects id
                var groups = data.Items.GroupBy(x => x.ID);
                foreach (var item in groups)
                {
                    if (item.Count() == 1) continue;
                    Debug.LogWarning($"item has duplicated ID: {item.Key}", item.Last());
                }

                Debug.Log("all items checked");
            }

            GUILayout.Space(10);
            DrawDefaultInspector();
        }
    }
}