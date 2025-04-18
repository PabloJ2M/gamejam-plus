namespace UnityEngine.Pool
{
    public class ItemBehaviour : MonoBehaviour, IPoolItem
    {
        public int Index { get; set; }
        public Transform Object => _transform;
        public IObjectPool<IPoolItem> Pool { get; set; }

        public bool IsActive { get => _transform.gameObject.activeSelf; set => _transform.gameObject.SetActive(value); }
        public Vector2 Position { get => _transform.position; set => _transform.position = value; }
        public Vector2 LocalPosition { get => _transform.localPosition; set => _transform.localPosition = value; }

        protected Transform _transform;

        protected virtual void Awake() => _transform = transform;
    }
}