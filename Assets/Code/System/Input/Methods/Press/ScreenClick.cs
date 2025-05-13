using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.InputSystem
{
    public class ScreenClick : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private UnityEvent _onClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            _onClick.Invoke();
        }
    }
}