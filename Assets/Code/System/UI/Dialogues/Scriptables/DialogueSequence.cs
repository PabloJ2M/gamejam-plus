using UnityEngine;

namespace UI.Dialogues
{
    [CreateAssetMenu(fileName = "dialogues", menuName = "System/Dialogues")]
    public class DialogueSequence : Dialogue
    {
        [SerializeField] private DialogueSingle[] _dialogues;

        public DialogueSingle[] Dialogues => _dialogues;
    }
}