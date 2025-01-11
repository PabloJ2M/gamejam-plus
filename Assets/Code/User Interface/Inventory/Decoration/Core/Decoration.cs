using UnityEngine.InputSystem;

namespace UnityEngine.DecorationSystem
{
    public class Decoration : TouchBehaviour
    {
        private SpriteRenderer _render;
        private ItemDec _item;

        public ItemDec Item => _item;
        public Vector2 Position { get => transform.position; set => transform.position = value; }

        protected override void Awake() { base.Awake(); _render = GetComponent<SpriteRenderer>(); }
        public void Setup(ItemDec item) { _item = item; _render.sprite = item.GetDirection(0); }

        protected override void OnSelect() => GetComponentInParent<Manager>().OnPickUp(this);
        protected override void OnDeselect() => GetComponentInParent<Manager>().OnDropItem(IsPointerOverUI());
    }
}