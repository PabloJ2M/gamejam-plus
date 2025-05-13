using System.Reflection;
using UnityEditor;

namespace UnityEngine.Tutorial
{
    [CustomEditor(typeof(IndicatorFitter))]
    public class IndicatorFitterEditor : Editor
    {
        protected const BindingFlags _flags = BindingFlags.NonPublic | BindingFlags.Instance;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            IndicatorFitter indicator = target as IndicatorFitter;
            var field = indicator.GetType().GetField("_tutorial", _flags).GetValue(indicator) as TutorialStep;

            if (field == null)
                EditorGUILayout.HelpBox("no tutorial added", MessageType.Warning);
            else if (field.Interaction != InteractionType.TextAndArrow && field.Interaction != InteractionType.WaitInteraction)
                EditorGUILayout.HelpBox("tutorial is not for display", MessageType.Error);
        }
    }
}