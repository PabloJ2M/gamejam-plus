using System.Collections.Generic;
using UnityEngine;

namespace UI.Effects
{
    public class TweenManager : MonoBehaviour
    {
        [SerializeField] private RectTransform[] _uiElements;
        private List<ITween> _tweens = new();

        private void Awake()
        {
            foreach (var uiElement in _uiElements)
                _tweens.Add(uiElement.GetComponent<ITween>());
        }

        public void Active() { foreach (var item in _tweens) item.Play(false); }
        public void Desactive() { foreach (var item in _tweens) item.Play(true); }
    }
}