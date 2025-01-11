using UnityEngine;

namespace Gameplay.Movement
{
    public class Room : MonoBehaviour
    {
        private RoomManager _manager;
        private Collider2D _area;

        private void Awake()
        {
            _area = GetComponent<Collider2D>();
            _manager = GetComponentInParent<RoomManager>();
        }

        public Collider2D Area => _area;
        public Vector2 Position => transform.position;
        public void SetRoom() => _manager?.ChangeRoom(this);
    }
}