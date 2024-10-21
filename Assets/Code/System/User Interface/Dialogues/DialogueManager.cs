using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Dialogues
{
    public class DialogueManager : SingletonBasic<DialogueManager>
    {
        [SerializeField, Range(0, 1)] private float _charDelay;
        [SerializeField, Range(0, 10)] private float _lastDelay;

        [SerializeField] private UnityEvent<bool> _onDisplay;

        private Queue<DialogueSingle> _listOfDialogues = new();
        private WaitForSeconds _textDelay, _waitDelay;
        private Coroutine _animation;
        private bool _skip;

        public Action<string> onHeaderChange, onTextChange;

        protected override void Awake() { base.Awake(); _textDelay = new(_charDelay); _waitDelay = new(_lastDelay); }

        public void SkipDialogue() => _skip = true;
        public void AddDialogue(DialogueSingle dialogue) => _listOfDialogues.Enqueue(dialogue);
        public void AddDialogue(DialogueSequence dialogues) { foreach (var item in dialogues.Dialogues) AddDialogue(item); }
        public void StartDialogue() { if (_animation == null) _animation = StartCoroutine(DisplayDialogues()); }

        private IEnumerator DisplayDialogues()
        {
            _onDisplay.Invoke(true);

            while (_listOfDialogues.Count > 0)
            {
                var dialogue = _listOfDialogues.Dequeue();
                onHeaderChange?.Invoke(dialogue.Header);
                string text = string.Empty;

                for (int i = 0; i < dialogue.Text.Length; i++)
                {
                    text += dialogue.Text[i];
                    onTextChange?.Invoke(text);
                    if (!_skip) yield return _textDelay;
                }

                yield return _waitDelay;
                _skip = false;
            }

            _animation = null;
            _onDisplay.Invoke(false);
        }
    }
}