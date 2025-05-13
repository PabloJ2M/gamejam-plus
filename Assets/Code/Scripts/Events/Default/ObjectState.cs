using UnityEngine;
using UnityEngine.Events;

namespace Events.Interact
{
    public class ObjectState : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onBegin, _onEnable, _onDisable;

        private void Start() => _onBegin?.Invoke();
        private void OnEnable() => _onEnable?.Invoke();
        private void OnDisable() => _onDisable?.Invoke();
    }
}