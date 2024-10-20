using UnityEngine;

namespace Controller
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Velocity : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _body;

        private void Awake() => _body = GetComponent<Rigidbody2D>();
        private void FixedUpdate() => _body.linearVelocityY = _speed;
    }
}