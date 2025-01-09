using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.GuitarHero
{
    public class TrackListener : MonoBehaviour
    {
        private TrackController _controller;
        private List<IPoolItem> _items = new();

        private void Awake() => _controller = GetComponentInParent<TrackController>();
        public void AddItem(IPoolItem item) => _items.Add(item);
        public void RemoveItem(IPoolItem item) => _items.Remove(item);

        public void CompareTrack(float target, float threshold)
        {
            if (_items.Count == 0) return;
            IPoolItem item = _items.First();
            float current = item.Position.y;

            if (current > target + threshold) return;
            if (current >= target - threshold * 0.5f) _controller?.SuccessTrack(); else _controller?.MissedTrack();

            _items.Remove(item);
            item.Pool.Release(item);
        }
    }
}