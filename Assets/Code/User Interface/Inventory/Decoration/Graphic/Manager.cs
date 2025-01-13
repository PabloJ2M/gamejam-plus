using System;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

namespace UnityEngine.DecorationSystem
{
    [RequireComponent(typeof(Tilemap))]
    public class Manager : MonoBehaviour
    {
        [SerializeField] private DecorationStorage _storage;
        [SerializeField] private Transform _parent;
        [SerializeField] private Decoration _prefab;

        [SerializeField] private float _threshold;
        [SerializeField] private LayerMask _detection;

        [Header("UI")]
        [SerializeField] private InventoryUI _inventory;
        [SerializeField] private UI_Handler _handler;

        public Decoration Selected { get; set; }
        public Vector2Int GridCursor { get; set; }
        public Vector2 CellSize => _tilemap.cellSize;
        public Vector2 TileAnchor => _tilemap.tileAnchor;
        public Vector2 WorldPoint => GridCursor + TileAnchor;

        public event Action<ItemDec> onSelect;
        public event Action<bool> onDrop;
        private Tilemap _tilemap;

        private void Awake() => _tilemap = GetComponent<Tilemap>();
        private void OnEnable() => _storage.Load();
        private void OnDisable() => _storage.Save();
        private void Start()
        {
            foreach (var item in _storage.Container.Items)
            {
                Decoration deco = Instantiate(_prefab, (Vector2)item.position + TileAnchor, default, _parent);
                deco.GridPosition = item.position;
                deco.Setup(item.item);
            }
        }

        public bool IsAvailable()
        {
            if (Selected == null) return false;

            Vector2 area = GetArea();
            if (area == Vector2.one) return IsTileAvailable() && IsAreaAvailable(Selected.Position, area);
            else return IsAreaOnTilemap(GridCursor, area) && IsAreaAvailable(Selected.Position, area);
        }
        public Vector2 GetArea()
        {
            if (!Selected) return Vector2.one;
            else if (!Selected.Item) return Vector2.one;
            return Selected.Item.GetSize();
        }
        private bool IsTileAvailable() => _tilemap.HasTile((Vector3Int)GridCursor);
        private bool IsAreaAvailable(Vector2 position, Vector2 size)
        {
            Collider2D[] collider = Physics2D.OverlapBoxAll(position, size - (Vector2.one * _threshold), 0, _detection, 0, 1);
            return collider.Length <= 2;
        }
        private bool IsAreaOnTilemap(Vector2 position, Vector2 size)
        {
            Vector3 min = position - size / 2f;
            Vector3 max = position + size / 2f;

            Vector3Int minCell = _tilemap.WorldToCell(min);
            Vector3Int maxCell = _tilemap.WorldToCell(max);

            for (int x = minCell.x; x <= maxCell.x; x++)
            {
                for (int y = minCell.y; y <= maxCell.y; y++) { if (!_tilemap.HasTile(new(x, y, 0))) return false; }
            }
            return true;
        }

        public void OnSelectItem(ItemDec item)
        {
            OnSelected(Instantiate(_prefab, WorldPoint, default, _parent));
            Selected.Setup(item);
        }
        public void OnPickUp(Decoration decoration)
        {
            _storage?.RemoveItem(new Deco(decoration.Item, decoration.GridPosition));
            _inventory?.Storage.Container.Add(decoration.Item, 1);
            _inventory?.RefreshUI();
            OnSelected(decoration);
        }

        private void OnSelected(Decoration decoration)
        {
            Selected = decoration;
            onSelect?.Invoke(decoration.Item);
        }
        public void OnDropItem(bool isOverUI)
        {
            onDrop?.Invoke(isOverUI);
            if (isOverUI || !IsAvailable()) { Destroy(Selected.gameObject); return; }

            _storage?.AddItem(new Deco(Selected.Item, GridCursor));
            _inventory?.Storage.Container.Remove(Selected.Item);
            _inventory?.RefreshUI();

            Selected.GridPosition = GridCursor;
            Selected.Position = WorldPoint;
            Selected = null;
        }
    }
}