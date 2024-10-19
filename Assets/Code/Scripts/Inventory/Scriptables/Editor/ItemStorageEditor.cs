using UnityEditor;
using UnityEngine;

namespace Inventory
{
    [CustomEditor(typeof(ItemStorage))]
    public class ItemStorageEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ItemStorage storage = target as ItemStorage;
            base.OnInspectorGUI();

            if (GUILayout.Button("Check ID's"))
                storage.CheckItemsID();
        }
    }
}