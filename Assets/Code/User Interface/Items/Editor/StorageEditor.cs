using System.Collections.Generic;
using UnityEditor;

namespace UnityEngine.InventorySystem
{
    [CustomEditor(typeof(SlotStorage))]
    public class StorageEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            SlotStorage storage = target as SlotStorage;
            List<int> keys = new List<int>(storage.Container.slots.Keys);
            List<Slot> values = new List<Slot>(storage.Container.slots.Values);

            GUIStyle backgroundStyle = new GUIStyle(GUI.skin.box);
            backgroundStyle.normal.background = MakeTex(2, 2, new Color(0.3f, 0.3f, 0.3f));
            backgroundStyle.padding = new RectOffset(10, 10, 5, 5);

            if (keys.Count == 0)
                EditorGUILayout.HelpBox("Storage is Empty", MessageType.Info);

            for (int i = 0; i < keys.Count; i++)
            {
                EditorGUILayout.BeginVertical(backgroundStyle);
                EditorGUILayout.LabelField($"Key {keys[i]}");
                EditorGUILayout.IntField("ID: ", values[i].ID);
                EditorGUILayout.IntField("Amount: ", values[i].Amount);
                EditorGUILayout.EndVertical();
            }
        }
        private Texture2D MakeTex(int width, int height, Color col)
        {
            Texture2D tex = new Texture2D(width, height);
            Color[] pix = tex.GetPixels();
            for (int i = 0; i < pix.Length; i++) pix[i] = col;
            tex.SetPixels(pix);
            tex.Apply();
            return tex;
        }
    }
}