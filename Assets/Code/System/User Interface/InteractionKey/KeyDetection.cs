using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace UI.Events
{
    public class KeyDetection : MonoBehaviour, IInteractable
    {
        [SerializeField] private InputSystem_Actions _input;
        [SerializeField] private bool _isUnique;
        [SerializeField] private UnityEvent _onInteract;

        public Vector2 WorldCoords => transform.position;
        private bool _locked, _enabled;

        private void Awake() => _input = new();
        private void Start() => _input.UI.Click.performed += OnInteract;
        //private void OnDestroy() => _input.UI.Click.performed -= OnInteract;

        public void OnEnter(Collider2D collider) { KeyInteraction.Instance?.SetInteractable(this); _enabled = true; }
        public void OnExit(Collider2D collider) { KeyInteraction.Instance?.RemoveInteractable(); _enabled = false; }

        void OnEnable() => _input.Enable();
        void OnDisable() => _input.Disable();

        private void OnInteract(InputAction.CallbackContext ctx)
        {
            if (_locked || !_enabled || Time.timeScale == 0) return;
            if (_isUnique) _locked = true;
            _onInteract.Invoke();
            OnExit(null);
        }
    }
}