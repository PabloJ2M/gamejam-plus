namespace UnityEngine.DecorationSystem
{
    public class Preview : MonoBehaviour
    {
        [SerializeField] private float _speed = 1;
        [SerializeField] private Color normal, error;

        private SpriteRenderer _shadow;
        private Transform _transform;
        private Manager _manager;

        private void Start() => Hide(false);
        private void OnEnable() { _manager.onSelect += Show; _manager.onDrop += Hide; }
        private void OnDisable() { _manager.onSelect -= Show; _manager.onDrop -= Hide; }

        private void Awake()
        {
            _transform = transform;
            _shadow = GetComponent<SpriteRenderer>();
            _manager = GetComponentInParent<Manager>();
        }
        private void Update()
        {
            if (!_manager.Selected) return;
            _manager.Selected.Position = Vector2.Lerp(_manager.Selected.Position, _transform.position, Time.deltaTime * _speed);
        }

        private void Show(ItemDec item) => _shadow.enabled = true;
        private void Hide(bool isOverUI) => _shadow.enabled = false;

        public void SetPosition()
        {
            _shadow.color = _manager.IsAvailable() ? normal : error;
            _transform.position = _manager.WorldPoint;
        }
    }
}