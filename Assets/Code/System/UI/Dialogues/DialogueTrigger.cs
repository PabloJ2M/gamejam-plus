using UnityEngine;

namespace UI.Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;

        private DialogueManager _manager;

        private void Awake() => _manager = DialogueManager.Instance;

        [ContextMenu("Start Dialogues")]
        public void TriggerDialogue()
        {
            if (_dialogue is DialogueSingle) _manager.AddDialogue(_dialogue as DialogueSingle);
            if (_dialogue is DialogueSequence) _manager.AddDialogue(_dialogue as DialogueSequence);
            _manager.StartDialogue();
        }
    }
}