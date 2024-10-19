using UnityEngine;

namespace UI.Effects
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeCanvas : Fade
    {
        [Header("Canvas Group")]
        [SerializeField] private bool _affectRaycast;
        [SerializeField] private bool _affectInteraction;

        [HideInInspector, SerializeField] private CanvasGroup _group;
        protected override float _alpha { get => _group.alpha; set => _group.alpha = value; }

        protected virtual void Awake() => _group = GetComponent<CanvasGroup>();
        protected virtual void OnEnable() => _onFade += OnFadeComplete;
        protected virtual void OnDisable() => _onFade -= OnFadeComplete;

        public void Default()
        {
            _alpha = 1;
            OnFadeComplete(true);
        }
        private void OnFadeComplete(bool value)
        {
            if (_affectInteraction) _group.interactable = value;
            if (_affectRaycast) _group.blocksRaycasts = value;
        }
    }
}