using UnityEngine;
using UnityEngine.Events;

namespace UI.Events
{
    public interface IInteractable
    {
        public Vector2 WorldCoords { get; }
    }

    public class KeyInteraction : SingletonBasic<KeyInteraction>
    {
        [SerializeField] private RectTransform _icon;
        [SerializeField] private UnityEvent _onDisplay, _onHide;

        private IInteractable _area;

        public void SetInteractable(IInteractable area) { _area = area; _onDisplay.Invoke(); }
        public void RemoveInteractable() { _area = null; _onHide.Invoke(); }

        private void Update()
        {
            if (_area == null) return;

            _icon.position = 1f * Vector2.up + _area.WorldCoords;
        }
    }
}