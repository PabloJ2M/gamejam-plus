using UnityEngine;
using UnityEngine.Events;

namespace UI.Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueSequence _dialogue;
        [SerializeField] private DialogueTrigger _sequence;
        [SerializeField] private bool _useIndicator;

        [SerializeField] private UnityEvent _onComplete;

        [ContextMenu("Start Dialogues")]
        public void TriggerDialogue()
        {
            DialogueManager manager = DialogueManager.Instance;
            if (!manager) return;

            manager?.AddDialogue(_dialogue, _useIndicator ? transform.position : Vector2.zero);
            manager?.StartDialogue(PerformeSequence);

            if (!_sequence) return;
            _sequence.TriggerDialogue();
        }

        private void PerformeSequence()
        {
            _onComplete.Invoke();
        }
    }
}