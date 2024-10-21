using UnityEngine;
using UnityEngine.Events;

namespace UI.Events
{
    public class KeyDetection : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool _isUnique;
        [SerializeField] private UnityEvent _onInteract;

        public Vector2 WorldCoords => transform.position;
        private KeyInteraction _interaction;
        private bool _locked, _enabled;

        private void Awake() => _interaction = KeyInteraction.Instance;

        public void OnEnter(Collider2D collider)
        {
            _interaction.onInteract += OnInteract;
            _interaction?.SetInteractable(this);
            _enabled = true;
        }
        public void OnExit(Collider2D collider)
        {
            _interaction.onInteract -= OnInteract;
            _interaction?.RemoveInteractable();
            _enabled = false;
        }

        private void OnInteract()
        {
            if (_locked || !_enabled || Time.timeScale == 0) return;
            if (_isUnique) _locked = true;
            _onInteract.Invoke();
            OnExit(null);
        }
    }
}