using UnityEditor;
using UnityEngine;

namespace Player.Data
{
    [CustomEditor(typeof(ResourceSender))]
    public class ResourceSenderEditor : Editor
    {
        public SerializedProperty scoreManager;
        public SerializedProperty resourcesToSave;

        private void OnEnable()
        {
            scoreManager = serializedObject.FindProperty("scoreManager");
            resourcesToSave = serializedObject.FindProperty("resourcesToSave");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(scoreManager);
            EditorGUILayout.PropertyField(resourcesToSave);

            if (GUILayout.Button("Log Resources")) LogResources();
            serializedObject.ApplyModifiedProperties();
        }

        public void LogResources()
        {
            string log = string.Empty;

            foreach (ResourceType resource in System.Enum.GetValues(typeof(ResourceType)))
                log += $"{resource}: {ResourceManager.GetResource(resource)}\r";

            Debug.Log(log);
        }
    }
}