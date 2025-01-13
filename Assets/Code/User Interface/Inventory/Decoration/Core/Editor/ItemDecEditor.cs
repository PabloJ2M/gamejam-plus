using System.Reflection;
using UnityEditor;
using UnityEngine.InventorySystem;

namespace UnityEngine.DecorationSystem
{
    [CustomEditor(typeof(ItemDec))]
    public class ItemDecEditor : ItemEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ItemDec item = target as ItemDec;

            FieldInfo backField = item.GetType().GetField("_back", _flags);
            backField?.SetValue(item, (Sprite)EditorGUILayout.ObjectField("Back:", (Sprite)backField?.GetValue(item), typeof(Sprite), false));

            FieldInfo leftField = item.GetType().GetField("_left", _flags);
            leftField?.SetValue(item, (Sprite)EditorGUILayout.ObjectField("Left:", (Sprite)leftField?.GetValue(item), typeof(Sprite), false));

            FieldInfo rightField = item.GetType().GetField("_right", _flags);
            rightField?.SetValue(item, (Sprite)EditorGUILayout.ObjectField("Right:", (Sprite)rightField?.GetValue(item), typeof(Sprite), false));
        }
    }
}