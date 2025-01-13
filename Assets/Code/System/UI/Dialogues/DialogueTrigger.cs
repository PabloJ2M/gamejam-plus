using UnityEngine;
using UnityEngine.Events;

namespace UI.Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private DialogueTrigger _sequence;

        [SerializeField] private UnityEvent _onBegin;

        private DialogueManager _manager;

        private void Awake() => _manager = DialogueManager.Instance;

        [ContextMenu("Start Dialogues")]
        public void TriggerDialogue()
        {
            if (_dialogue is DialogueSingle) _manager.AddDialogue(_dialogue as DialogueSingle);
            if (_dialogue is DialogueSequence) _manager.AddDialogue(_dialogue as DialogueSequence);
            _manager.StartDialogue(_onBegin.Invoke, PerformeSequence);
        }
        private void PerformeSequence()
        {
            if (!_sequence) return;
            _sequence.TriggerDialogue();
        }
    }
}