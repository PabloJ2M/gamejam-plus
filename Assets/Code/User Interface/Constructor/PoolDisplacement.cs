namespace UnityEngine.Pool
{
    public class PoolDisplacement : MonoBehaviour
    {
        [SerializeField] private Direction _direction;
        [SerializeField] private bool _isLocalPosition;
        [SerializeField] private float _defaultSpeed;

        private IPoolBehaviour _poolReference;
        private Vector2 _moveDirection;
        private float _multiply = 1;

        public float Speed { get => _multiply * _defaultSpeed * Time.fixedDeltaTime; set => _multiply = value; }

        private void Awake() => _poolReference = GetComponent<IPoolBehaviour>();
        private void Start() => _moveDirection = _direction.GetDirection();

        private void FixedUpdate()
        {
            Vector2 velocity = _moveDirection * Speed;

            foreach (var item in _poolReference.ActiveItems)
            {
                if (!_isLocalPosition) item.Position += velocity;
                else item.LocalPosition += velocity;
            }
        }
    }
}