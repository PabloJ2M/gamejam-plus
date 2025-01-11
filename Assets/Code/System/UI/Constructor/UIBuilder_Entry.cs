using UnityEngine.EventSystems;
using UnityEngine.Pool;

namespace UnityEngine.UI
{
    public abstract class UI_Entry<T> : ItemBehaviour
    {
        public int GetIndex() => _transform.GetSiblingIndex();
        public abstract void Setup(T data);
    }

    public abstract class UI_EntryInteract<T> : UI_Entry<T>, IPointerDownHandler
    {
        protected abstract void OnInteract();
        public void OnPointerDown(PointerEventData eventData) => OnInteract();
    }
}