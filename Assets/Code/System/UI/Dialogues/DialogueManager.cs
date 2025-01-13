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

        private Queue<(DialogueSequence, Vector2)> _listOfDialogues = new();
        private Queue<Action> _listOfActions = new();

        private WaitForSeconds _textDelay;
        private bool _isAnimated, _skip, _next;

        public Action<string> onHeaderChange, onTextChange;
        public Action<Vector2> onDisplayIndicator;

        protected override void Awake() { base.Awake(); _textDelay = new(_charDelay); }

        public void SkipDialogue() { if (!_skip) _skip = true; else _next = true; }
        public void AddDialogue(DialogueSequence dialogue, Vector2 coords) => _listOfDialogues.Enqueue((dialogue, coords));
        public void StartDialogue(Action onComplete) { _listOfActions.Enqueue(onComplete); StartCoroutine(DisplayDialogues()); }

        private IEnumerator DisplayDialogues()
        {
            if (_isAnimated || _listOfDialogues.Count == 0) yield break;
            _isAnimated = true;

            do {
                _onDisplay.Invoke(true);

                var dialogue = _listOfDialogues.Dequeue();
                onDisplayIndicator.Invoke(dialogue.Item2);

                foreach (var line in dialogue.Item1.Dialogues)
                {
                    string messageComplete = string.Format(line.Text, SetUserName.Instance.username);
                    onHeaderChange?.Invoke(line.Header);
                    string text = string.Empty;

                    for (int i = 0; i < messageComplete.Length; i++)
                    {
                        text += messageComplete[i];
                        onTextChange?.Invoke(text);
                        if (!_skip) yield return _textDelay;
                    }

                    yield return new WaitUntil(() => _next);
                    _next = _skip = false;
                }

                _listOfActions.Dequeue()?.Invoke();
            }
            while (_listOfActions.Count > 0);

            _onDisplay.Invoke(false);
            _isAnimated = false;
        }
    }
}