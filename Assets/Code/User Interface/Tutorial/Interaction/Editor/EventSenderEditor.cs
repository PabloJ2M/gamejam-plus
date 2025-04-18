using System.Reflection;
using UnityEditor;

namespace UnityEngine.Tutorial.UI
{
    [CustomEditor(typeof(EventSender))]
    public class EventSenderEditor : Editor
    {
        protected const BindingFlags _flags = BindingFlags.NonPublic | BindingFlags.Instance;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EventSender sender = target as EventSender;
            var field = sender.GetType().GetField("_tutorial", _flags).GetValue(sender) as TutorialStep;

            if (field == null)
                EditorGUILayout.HelpBox("no tutorial added", MessageType.Warning);
            else if (field.Interaction != InteractionType.WaitInteraction)
                EditorGUILayout.HelpBox("tutorial is not interactable", MessageType.Error);
        }
    }
}