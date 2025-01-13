using UnityEngine;

namespace UI.Dialogues
{
    [CreateAssetMenu(fileName = "dialogues", menuName = "system/dialogues")]
    public class DialogueSequence : ScriptableObject
    {
        [SerializeField] private Dialogue[] _dialogues;

        public Dialogue[] Dialogues => _dialogues;
    }
}