using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.GuitarHero
{
    public class TrackItem : ItemBehaviour
    {
        private TrackController _controller;
        private TrackListener _listener;
        private RectTransform _rect;

        protected override void Awake()
        {
            base.Awake();
            _rect = GetComponent<RectTransform>();
            _controller = GetComponentInParent<TrackController>();
        }
        private void LateUpdate()
        {
            if (LocalPosition.y >= _controller.Limit - _rect.rect.height) return;
            OnRelease(); Pool.Release(this);
        }

        public void OnGet()
        {
            _listener = GetComponentInParent<TrackListener>();
            _listener.AddItem(this);
        }
        public void OnRelease()
        {
            _listener.RemoveItem(this);
        }
    }
}