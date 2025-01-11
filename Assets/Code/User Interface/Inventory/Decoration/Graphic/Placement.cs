using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

namespace UnityEngine.DecorationSystem
{
    [RequireComponent(typeof(Tilemap))]
    public class Placement : PointerBehaviour
    {
        private Manager _manager;
        private Preview _preview;
        private Camera _camera;

        protected override void Awake()
        {
            base.Awake();
            _camera = Camera.main;
            _manager = GetComponent<Manager>();
            _preview = GetComponentInChildren<Preview>();
        }
        protected override void OnEnable() { base.OnEnable(); _manager.onSelect += SetItemPosition; }
        protected override void OnDisable() { base.OnDisable(); _manager.onSelect -= SetItemPosition; }
        private void SetItemPosition(ItemDec item) => _preview?.SetPosition();

        protected override void OnPointer(InputAction.CallbackContext ctx)
        {
            Vector2 world = _camera.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            Vector2 grid = world - _manager.TileAnchor;

            grid.x = Mathf.Round(grid.x / _manager.CellSize.x) * _manager.CellSize.x;
            grid.y = Mathf.Round(grid.y / _manager.CellSize.y) * _manager.CellSize.y;
            _manager.GridCursor = new Vector2Int((int)grid.x, (int)grid.y);

            if (_manager.Selected == null) return;
            SetItemPosition(null);
        }
    }
}