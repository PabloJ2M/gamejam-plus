using System;
using UnityEngine;

namespace Events
{
    [Flags] public enum Interaction { Enter = 1, Stay = 2, Exit = 4 }

    [RequireComponent(typeof(Collider2D))]
    public abstract class PhysicsCollide : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private Interaction _interaction;

        public Action<Collider2D> onEnter, onExit;

        protected void OnCollideEnter(Collider2D other)
        {
            if (!_interaction.HasFlag(Interaction.Enter)) return;
            if (!_layer.CompareLayer(other.gameObject)) return;
            onEnter?.Invoke(other);
        }
        protected void OnCollideExit(Collider2D other)
        {
            if (!_interaction.HasFlag(Interaction.Enter)) return;
            if (!_layer.CompareLayer(other.gameObject)) return;
            onExit?.Invoke(other);
        }
    }
}