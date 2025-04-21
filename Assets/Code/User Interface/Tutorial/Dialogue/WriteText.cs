using System.Collections;
using UnityEngine.Events;

namespace UnityEngine.Tutorial.UI
{
    public class WriteText : TutorialBehaviour
    {
        [SerializeField, Range(0f, 0.5f)] private float _charDelay;
        [SerializeField] private UnityEvent<bool> _onDisplay;
        [SerializeField] private UnityEvent<string> _onValueChanged;

        private WaitForSecondsRealtime _waitCharDelay;
        private WaitUntil _waitUntilNext;
        private bool _skip, _next;

        protected override void Awake() { base.Awake(); _waitCharDelay = new(_charDelay); _waitUntilNext = new(() => _next); }
        protected override void OnPerformeTutorial(TutorialStep step) => StartCoroutine(MessageSequence(step));
        private void Start() { _onDisplay.Invoke(false); _onValueChanged.Invoke(string.Empty); }

        [ContextMenu("Skip Message")] public void SkipMessage() { if (!_skip) _skip = true; else _next = true; }

        private IEnumerator MessageSequence(TutorialStep step)
        {
            _onDisplay.Invoke(true);

            for (int i = 0; i < step.Messages.Length; i++)
            {
                yield return WriteAnimation(step.Messages[i]);
                yield return _waitUntilNext;
                _skip = _next = false;
            }

            if (step.Interaction != InteractionType.WaitInteraction) _controller.Continue = true;
            else step.onDisplayAnimation?.Invoke();

            _onDisplay.Invoke(false);
        }
        private IEnumerator WriteAnimation(string message)
        {
            string lineText = string.Empty;

            for (int i = 0; i < message.Length; i++)
            {
                lineText += message[i];
                _onValueChanged.Invoke(lineText);
                if (!_skip) yield return _waitCharDelay;
            }

            _skip = true;
        }
    }
}