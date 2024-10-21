using UnityEngine;
using UnityEngine.Events;

namespace Events.Interact
{
    public class ObjectState : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onBegin;

        private void Start() => _onBegin?.Invoke();
    }
}