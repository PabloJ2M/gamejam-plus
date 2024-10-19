using UnityEngine;
using TMPro;

namespace UI.Dialogues
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        private DialogueManager _manager;

        private void Awake() => _manager = DialogueManager.Instance;

        private void OnEnable()
        {
            _manager.onHeaderChange += OnHeaderChange;
            _manager.onTextChange += OnTextChange;
        }

        private void OnHeaderChange(string value) => _headerText.SetText(value);
        private void OnTextChange(string value) => _dialogueText.SetText(value);
        public void OnSkipButton() => _manager?.SkipDialogue();
    }
}