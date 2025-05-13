using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.Events
{
    public class ObjectDificulty : PoolBehaviour
    {
        [SerializeField] private RectTransform _hiddenParent;
        [SerializeField, Range(0, 10)] private int _maxAmount;
        [SerializeField] private int _level = 1;

        private int _current;

        protected override void OnGetFromPool(IPoolItem item)
        {
            base.OnGetFromPool(item);
            if (_hiddenParent) item.Object.SetParent(_parent);
        }
        protected override void OnReleaseFromPool(IPoolItem item)
        {
            base.OnReleaseFromPool(item);
            if (_hiddenParent) item.Object.SetParent(_hiddenParent);
        }

        public void ResetDificulty() { _current = 0; ClearItems(); }
        public void AddDificulty()
        {
            _current++;
            if (_current % _level != 0) return;
            if (_parent.childCount >= _maxAmount) return;
            Pool.Get();
        }
    }
}