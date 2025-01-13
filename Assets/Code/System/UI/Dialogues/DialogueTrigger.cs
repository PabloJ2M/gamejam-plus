using UnityEngine;
using UnityEngine.Events;

namespace UI.Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private DialogueSequence _dialogue;
        [SerializeField] private DialogueTrigger _sequence;

        [SerializeField] private UnityEvent _onBegin, _onComplete;

        [ContextMenu("Start Dialogues")]
        public void TriggerDialogue()
        {
            DialogueManager.Instance.AddDialogue(_dialogue);
            DialogueManager.Instance.StartDialogue(_onBegin.Invoke, PerformeSequence);
        }
        private void PerformeSequence()
        {
            _onComplete.Invoke();
            if (!_sequence) return;
            _sequence.TriggerDialogue();
        }
    }
}