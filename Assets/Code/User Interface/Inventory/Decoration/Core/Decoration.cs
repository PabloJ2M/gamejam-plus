using UnityEngine.InputSystem;

namespace UnityEngine.DecorationSystem
{
    public class Decoration : TouchBehaviour
    {
        private SpriteRenderer _render;
        private BoxCollider2D _collider;
        private ItemDec _item;

        public ItemDec Item => _item;
        public Vector2 Position { get => transform.position; set => transform.position = value; }
        public Vector2Int GridPosition { get; set; }

        protected override void Awake() { base.Awake(); _render = GetComponent<SpriteRenderer>(); _collider = GetComponent<BoxCollider2D>(); }
        protected override void OnSelect() => GetComponentInParent<Manager>().OnPickUp(this);
        protected override void OnDeselect() => GetComponentInParent<Manager>().OnDropItem(IsPointerOverUI());

        public void Setup(ItemDec item)
        {
            _item = item;
            _render.sprite = item.GetDirection(0);
            _collider.size = item.GetSize();
        }
    }
}