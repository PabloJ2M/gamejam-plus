using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.GuitarHero
{
    public class TrackItem : MonoBehaviour, IPoolItem
    {
        public GameObject Object => _rect.gameObject;
        public bool IsActive { set => _rect.gameObject.SetActive(value); }

        public IObjectPool<IPoolItem> Pool { get; set; }
        public Vector2 Position { get => _rect.position; set => _rect.position = value; }
        public Vector2 LocalPosition { get => _rect.localPosition; set => _rect.localPosition = value; }

        private TrackController _controller;
        private TrackListener _listener;
        private RectTransform _rect;

        private void Awake()
        {
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