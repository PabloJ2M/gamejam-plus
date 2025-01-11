using UnityEngine;
using UnityEngine.Events;

namespace Events.Interact
{
    public class Colors : MonoBehaviour
    {
        [SerializeField] private Color _on = Color.green;
        [SerializeField] private Color _off = Color.red;

        [SerializeField] private UnityEvent<Color> _onValueChanged;

        public void SetStatus(bool value) => _onValueChanged.Invoke(value ? _on : _off);
    }
}