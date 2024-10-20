using UnityEngine;

namespace Events
{
    public class ColliderEvent : PhysicsCollide
    {
        private void OnCollisionEnter2D(Collision2D collision) => OnCollideEnter(collision.collider);
        private void OnCollisionExit2D(Collision2D collision) => OnCollideExit(collision.collider);
    }
}