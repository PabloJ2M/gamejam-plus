using System;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Events
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class InteractRenderer : MonoBehaviour, IInteractable
    {
        [SerializeField] private bool _needConfirmation;
        [SerializeField] private UnityEvent _onInteract;

        public int ID => GetInstanceID();
        public Vector2 WorldCoords => transform.position + Vector3.up;
        public Action Action => _onInteract.Invoke;
        public bool NeedConfirmation => _needConfirmation;

        private InteractManager _interaction;

        private void Awake() => _interaction = InteractManager.instance;
        private void OnBecameVisible() => _interaction.AddInteraction(this);
        private void OnBecameInvisible() => _interaction.RemoveInteraction(this);
    }
}