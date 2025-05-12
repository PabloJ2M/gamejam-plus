using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Events
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class InteractRenderer : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool _needConfirmation;
        [SerializeField, Range(0, 1)] private float _upDistance = 1;

        [SerializeField] private UnityEvent _onInteract;
        private bool _isLocked, _isSeened, _isRendered;

        public int ID => GetInstanceID();
        public Vector2 WorldCoords => transform.position + (Vector3.up * _upDistance);
        public Action Action => _onInteract.Invoke;
        public bool NeedConfirmation => _needConfirmation;

        private InteractManager _interaction;

        private void Awake() => _interaction = InteractManager.instance;
        private void OnBecameVisible() => RenderStatus(true);
        private void OnBecameInvisible() => RenderStatus(false);

        private void RenderStatus(bool value)
        {
            _isSeened = value;
            if (_isLocked || _isRendered == value) return;

            if (value) _interaction.AddInteraction(this);
            else _interaction.RemoveInteraction(this);
            _isRendered = value;
        }

        public void OnStatus(bool value)
        {
            _isLocked = value;
            if (value) { _isRendered = false; _interaction.RemoveInteraction(this); }
            else if (_isSeened && !_isRendered) { _isRendered = true; _interaction.AddInteraction(this); }
        }
    }
}