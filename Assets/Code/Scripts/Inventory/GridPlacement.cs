using UnityEngine;
using UnityEngine.InputSystem;

namespace Inventory.Build
{
    [RequireComponent(typeof(Grid))]
    public class GridPlacement : MonoBehaviour
    {
        [SerializeField] private Vector3 _tileAnchor = new Vector3(0.5f, 0.5f);
        public Transform ObjectSelected { private get; set; }
        
        private Grid _manager;
        private Camera _camera;
        private Actions _actions;

        private void Awake() { _actions = new(); _camera = Camera.main; _manager = GetComponentInParent<Grid>(); }
        private void Start() => _actions.UI.Point.performed += OnDrag;
        private void OnEnable() => _actions.Enable();
        private void OnDisable() => _actions.Disable();

        private void OnDrag(InputAction.CallbackContext ctx)
        {
            if (ObjectSelected == null) return;

            Vector2 input = ctx.ReadValue<Vector2>();
            Vector2 pos = _camera.ScreenToWorldPoint(input) - _tileAnchor;

            pos.x = Mathf.Round(pos.x / _manager.cellSize.x) * _manager.cellSize.x;
            pos.y = Mathf.Round(pos.y / _manager.cellSize.y) * _manager.cellSize.y;
            ObjectSelected.position = new Vector2(pos.x + _tileAnchor.x, pos.y + _tileAnchor.y);
        }
    }
}