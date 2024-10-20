using System;
using UnityEngine;
using UnityEngine.Events;

namespace Events.Physic
{
    [Flags] public enum Interaction { Enter = 1, Stay = 2, Exit = 4 }

    [RequireComponent(typeof(Collider2D))]
    public abstract class PhysicsCollide : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private Interaction _interaction;
        [SerializeField] private bool _destroyOther = false;

        public UnityEvent<Collider2D> onEnter, onExit;

        protected void OnCollideEnter(Collider2D other)
        {
            if (!_interaction.HasFlag(Interaction.Enter)) return;
            if (!_layer.CompareLayer(other.gameObject)) return;
            onEnter?.Invoke(other);

            if (_destroyOther) Destroy(other.gameObject);
        }
        protected void OnCollideExit(Collider2D other)
        {
            if (!_interaction.HasFlag(Interaction.Enter)) return;
            if (!_layer.CompareLayer(other.gameObject)) return;
            onExit?.Invoke(other);

            if (_destroyOther) Destroy(other.gameObject);
        }
    }
}