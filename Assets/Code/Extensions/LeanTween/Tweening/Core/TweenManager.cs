using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Effects
{
    public class TweenManager : MonoBehaviour
    {
        [SerializeField] private RectTransform[] _uiElements;
        [SerializeField] private UnityEvent<bool> _onValueChanged;
        private List<ITween> _tweens = new();

        private void Awake()
        {
            foreach (var uiElement in _uiElements)
                _tweens.Add(uiElement.GetComponent<ITween>());
        }

        public void Active() { _onValueChanged.Invoke(false); foreach (var item in _tweens) item.Play(false); }
        public void Desactive() { _onValueChanged.Invoke(true); foreach (var item in _tweens) item.Play(true); }
    }
}