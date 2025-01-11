using UnityEngine.Events;

namespace UnityEngine.InputSystem
{
    public class ScreenTap : TouchBehaviour
    {
        [SerializeField] private UnityEvent _onPress;

        protected override void OnSelect() => _onPress.Invoke();
        protected override void OnDeselect() { }
    }
}