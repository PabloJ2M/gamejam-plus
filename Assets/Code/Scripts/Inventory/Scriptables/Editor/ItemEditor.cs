using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Inventory
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

            FieldInfo imageField = item.GetType().GetField("_icon", _flags);
            imageField?.SetValue(item, (Sprite)EditorGUILayout.ObjectField("Icon", (Sprite)imageField?.GetValue(item), typeof(Sprite), false));

            FieldInfo nameField = item.GetType().GetField("_name", _flags);
            nameField?.SetValue(item, EditorGUILayout.TextField("Name", (string)nameField?.GetValue(item)));

            FieldInfo typeField = item.GetType().GetField("_type", _flags);
            typeField?.SetValue(item, EditorGUILayout.EnumPopup("Type", (ItemType)typeField?.GetValue(item)));

            FieldInfo costField = item.GetType().GetField("_cost", _flags);
            costField?.SetValue(item, Mathf.Clamp(EditorGUILayout.IntField("Cost", (int)costField?.GetValue(item)), 0, int.MaxValue));

            EditorGUILayout.Space(10);
            if (GUILayout.Button("Get Random ID")) item?.GetRandomID();

            if (!GUI.changed) return;

            EditorGUIUtility.SetIconForObject(item, item.Icon ? item.Icon.texture : null);
            EditorUtility.SetDirty(target);
        }
    }
}