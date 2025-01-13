using UnityEngine;
using UI.Display;
using UnityEditor;

public class SaveScoreToResources : MonoBehaviour
{
    [SerializeField] public Score scoreManager;
    [SerializeField] CraftingResources[] resourcesToSave;

    public void SaveResources()
    {
        for(int i = 0; i < resourcesToSave.Length; i++)
        {
            CraftingResourcesManager.AddResource(resourcesToSave[i], scoreManager.score);
        }
    }
}

[CustomEditor(typeof(SaveScoreToResources))]
public class SaveScoreToResourcesEditor : Editor
{
    public SerializedProperty scoreManager;
    public SerializedProperty resourcesToSave;

    void OnEnable()
    {
        scoreManager = serializedObject.FindProperty("scoreManager");
        resourcesToSave = serializedObject.FindProperty("resourcesToSave");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(scoreManager);
        EditorGUILayout.PropertyField(resourcesToSave);

        if(GUILayout.Button("Log Resources"))
        {
            LogResources();
        }

        serializedObject.ApplyModifiedProperties();
    }

   public void LogResources()
   {
        string log = string.Empty;

        foreach(CraftingResources resource in System.Enum.GetValues(typeof(CraftingResources)))
        {
            log += resource.ToString() + ": " + CraftingResourcesManager.GetResource(resource) + " ";
        }

        Debug.Log(log);
   }
}
