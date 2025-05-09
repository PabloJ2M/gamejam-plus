using UnityEngine;
using UnityEngine.Events;

namespace UI.Effects
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeCanvas : Fade
    {
        [Header("Canvas Group")]
        [SerializeField] private bool _affectRaycast;
        [SerializeField] private bool _affectInteraction;
        [SerializeField] private UnityEvent _onFadeIn, _onFadeOut;

        [HideInInspector, SerializeField] private CanvasGroup _group;
        protected override float _alpha { get => _group.alpha; set { if (_group) _group.alpha = value; } }

        protected virtual void Awake() => _group = GetComponent<CanvasGroup>();
        protected virtual void OnEnable() => _onFade += OnFadeComplete;
        protected virtual void OnDisable() => _onFade -= OnFadeComplete;
        protected override void OnComplete() { base.OnComplete(); if (_isVisible) _onFadeIn.Invoke(); else _onFadeOut.Invoke(); }

        public void Default()
        {
            _alpha = 0;
            OnFadeComplete(false);
        }
        private void OnFadeComplete(bool value)
        {
            if (_group == null) return;
            if (_affectInteraction) _group.interactable = value;
            if (_affectRaycast) _group.blocksRaycasts = value;
        }
    }
}