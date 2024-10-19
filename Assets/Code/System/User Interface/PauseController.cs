using UnityEngine;
using UnityEngine.InputSystem;
using UI.Effects;

namespace UI.Controller
{
    public class PauseController : FadeCanvas
    {
        public bool IsPaused { get; private set; }

        #region Input System CallbackContext
        private InputSystem_Actions _inputActions;

        protected override void Awake() { base.Awake(); _inputActions = new(); }
        protected override void OnEnable() { base.OnEnable(); _inputActions.Enable(); }
        protected override void OnDisable() { base.OnDisable(); _inputActions.Disable(); }
        protected override void Start() { base.Start(); _inputActions.UI.Escape.performed += PauseState; }
        private void PauseState(InputAction.CallbackContext value) => PauseState();
        #endregion

        public void PauseState() => PauseSate(!IsPaused);

        public void PauseSate(bool value)
        {
            Play(value);
            IsPaused = value;
            Time.timeScale = value ? 0 : 1;
        }
    }
}