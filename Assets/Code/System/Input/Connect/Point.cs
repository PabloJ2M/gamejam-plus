using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Inputs.Drag
{
    public class Point : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private OnConnect _manager;

        public int Index => transform.GetSiblingIndex();
        public Vector2 Position => transform.position;

        private void Awake() => _manager = GetComponentInParent<OnConnect>();
        private void SendPoint() { if (_manager.IsPressing) _manager.AddPoint(this); }

        public void OnPointerEnter(PointerEventData eventData) => SendPoint();
        public void OnPointerExit(PointerEventData eventData) => SendPoint();
    }
}