using UnityEngine;
using UnityEngine.Pool;
using UI.Effects;

namespace Gameplay.Events
{
    public class InteractDisplay : ItemBehaviour
    {
        [SerializeField] private FadeCanvas _canvas;
        private InteractManager _manager;
        private IInteractable _reference;

        protected override void Awake() { base.Awake(); _manager = GetComponentInParent<InteractManager>(); }
        private void Update() { if (_reference != null) _transform.position = _reference.WorldCoords; }

        public void Hide() { _canvas.Default(); _reference = null; Pool.Release(this); }
        public void SetUp(IInteractable reference) { _reference = reference; _canvas.FadeIn(); }
        public void Select()
        {
            if (!_reference.NeedConfirmation) _reference.Action.Invoke();
            else _manager.ConfirmInteraction(_reference);
        }
    }
}