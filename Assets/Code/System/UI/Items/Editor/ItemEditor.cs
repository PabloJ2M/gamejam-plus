using System.Reflection;
using UnityEditor;

namespace UnityEngine.InventorySystem
{
    [CustomEditor(typeof(Item))]
    public class ItemEditor : Editor
    {
        private const BindingFlags _flags = BindingFlags.NonPublic | BindingFlags.Instance;

        public override void OnInspectorGUI()
        {
            Item item = target as Item;

            FieldInfo idField = item.GetType().GetField("_id", _flags);
            EditorGUILayout.LabelField($"item id: {(int)idField?.GetValue(item)}");

            FieldInfo imageField = item.GetType().GetField("_image", _flags);
            imageField?.SetValue(item, (Sprite)EditorGUILayout.ObjectField("Image:", (Sprite)imageField?.GetValue(item), typeof(Sprite), false));

            FieldInfo nameField = item.GetType().GetField("_name", _flags);
            nameField?.SetValue(item, EditorGUILayout.TextField("Name:", (string)nameField?.GetValue(item)));

            FieldInfo maxField = item.GetType().GetField("_max", _flags);
            maxField?.SetValue(item, Mathf.Clamp(EditorGUILayout.IntField("Max Slot:", (int)maxField?.GetValue(item)), 0, int.MaxValue));

            FieldInfo costField = item.GetType().GetField("_cost", _flags);
            costField?.SetValue(item, Mathf.Clamp(EditorGUILayout.IntField("Cost:", (int)costField?.GetValue(item)), 0, int.MaxValue));

            EditorGUILayout.Space(10);
            if (GUILayout.Button("Get Random ID")) item?.SetRandomID();

            if (!GUI.changed) return;

            EditorGUIUtility.SetIconForObject(item, item.Image ? item.Image.texture : null);
            EditorUtility.SetDirty(target);
        }
    }
}