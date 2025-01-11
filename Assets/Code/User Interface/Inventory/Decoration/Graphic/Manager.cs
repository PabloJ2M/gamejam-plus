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
                Instantiate(_prefab, (Vector2)item.position + TileAnchor, default, _parent).Setup(item.item);
        }

        public bool IsAvailable() => _tilemap.HasTile((Vector3Int)GridCursor) && _storage.CanPlaceItem(GridCursor);
        public void OnSelectItem(ItemDec item)
        {
            OnSelected(Instantiate(_prefab, WorldPoint, default, _parent));
            Selected.Setup(item);
        }
        public void OnPickUp(Decoration decoration)
        {
            _storage?.RemoveItem(new Deco(decoration.Item, GridCursor));
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

            Selected.Position = WorldPoint;
            Selected = null;
        }
    }
}