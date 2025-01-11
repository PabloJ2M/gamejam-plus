using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Events
{
    [DefaultExecutionOrder(-10)]
    public class InteractManager : UI_Builder
    {
        [SerializeField] private GameObject _confirmView;
        private IInteractable _confirmation;

        public static InteractManager instance;

        protected override void Awake() { base.Awake(); instance = this; }
        protected override void OnDisplay() { }

        public void ConfirmInteraction(IInteractable action) { _confirmation = action; _confirmView.SetActive(true); }
        public void ExecuteConfirmation() { if (_confirmation == null) return; _confirmation.Action?.Invoke(); CancelConfirmation(); }
        public void CancelConfirmation() { _confirmation = null; _confirmView.SetActive(false); }
        
        public void AddInteraction(IInteractable interactable)
        {
            InteractDisplay item = Pool.Get() as InteractDisplay;
            item.Index = interactable.ID;
            item.SetUp(interactable);
        }
        public void RemoveInteraction(IInteractable interactable)
        {
            int index = ActiveItems.FindIndex(x => x.Index == interactable.ID);
            if (index < 0) return;

            InteractDisplay item = ActiveItems[index] as InteractDisplay;
            item.Hide();
        }
    }
}