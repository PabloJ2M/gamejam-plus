using UnityEngine;
using TMPro;

namespace UI.Dialogues
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private RectTransform _indicator;

        private DialogueManager _manager;
        private RectTransform _canvas;
        private Camera _camera;

        private void Awake() => _manager = DialogueManager.Instance;
        private void Start() => _camera = Camera.main;

        private void OnEnable()
        {
            _canvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
            _manager.onDisplayIndicator += OnDisplayIndicator;
            _manager.onHeaderChange += OnHeaderChange;
            _manager.onTextChange += OnTextChange;
        }

        private void OnDisplayIndicator(Vector2 coords)
        {
            Vector2 screen = _camera.WorldToScreenPoint(coords);
            _indicator.position = screen;
            _indicator.gameObject.SetActive(coords != Vector2.zero);
        }
        private void OnHeaderChange(string value) => _headerText?.SetText(value);
        private void OnTextChange(string value) => _dialogueText?.SetText(value);
        public void OnSkipButton() => _manager?.SkipDialogue();
    }
}