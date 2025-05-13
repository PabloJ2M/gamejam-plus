using System.Reflection;
using UnityEditor;

namespace UnityEngine.InventorySystem
{
    [CustomEditor(typeof(Item))]
    public class ItemEditor : Editor
    {
        protected const BindingFlags _flags = BindingFlags.NonPublic | BindingFlags.Instance;

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

            FieldInfo researchField = item.GetType().GetField("_coins", _flags);
            researchField?.SetValue(item, (int)EditorGUILayout.Slider("Coins:", (int)researchField?.GetValue(item), 0, 100));

            FieldInfo resourceField = item.GetType().GetField("_hearts", _flags);
            resourceField?.SetValue(item, (int)EditorGUILayout.Slider("Hearts:", (int)resourceField?.GetValue(item), 0, 100));

            EditorGUILayout.Space(10);
            if (GUILayout.Button("Get Random ID")) item?.SetRandomID();

            if (!GUI.changed) return;
            EditorUtility.SetDirty(target);
        }
    }
}