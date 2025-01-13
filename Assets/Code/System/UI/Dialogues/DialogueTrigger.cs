using UnityEngine;
using UnityEngine.Events;

namespace UI.Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private DialogueTrigger _sequence;

        [SerializeField] private UnityEvent _onBegin, _onComplete;

        private DialogueManager _manager;

        private void Awake() => _manager = DialogueManager.Instance;

        [ContextMenu("Start Dialogues")]
        public void TriggerDialogue()
        {
            if (_dialogue is DialogueSingle single) _manager.AddDialogue(single);
            if (_dialogue is DialogueSequence sequence) _manager.AddDialogue(sequence);
            _manager.StartDialogue(_onBegin.Invoke, PerformeSequence);
        }
        private void PerformeSequence()
        {
            _onComplete.Invoke();
            if (!_sequence) return;
            _sequence.TriggerDialogue();
        }
    }
}