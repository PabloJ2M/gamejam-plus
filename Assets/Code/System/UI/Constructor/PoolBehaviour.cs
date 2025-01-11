using System.Collections.Generic;

namespace UnityEngine.Pool
{
    public abstract class PoolCore : MonoBehaviour, IPoolBehaviour
    {
        protected IObjectPool<IPoolItem> _pooling;
        private List<IPoolItem> _activeItems = new();

        public IObjectPool<IPoolItem> Pool => _pooling;
        public List<IPoolItem> ActiveItems => _activeItems;

        protected abstract IPoolItem CreateItem();
        protected virtual void OnGetFromPool(IPoolItem item) { item.IsActive = true; _activeItems.Add(item); }
        protected virtual void OnReleaseFromPool(IPoolItem item) { item.IsActive = false; _activeItems.Remove(item); }
        protected virtual void OnDestroyPooledObject(IPoolItem item) => Destroy(item.Object);

        public void ClearItems() { for (int i = _activeItems.Count - 1; i >= 0; i--) OnReleaseFromPool(_activeItems[i]); }
    }
    public abstract class PoolBehaviour : PoolCore
    {
        [SerializeField] protected RectTransform _parent;
        [SerializeField] protected GameObject _prefab;

        protected virtual void Awake() => _pooling = new ObjectPool<IPoolItem>
            (CreateItem, OnGetFromPool, OnReleaseFromPool, OnDestroyPooledObject, true, 20, 100);

        protected override IPoolItem CreateItem()
        {
            IPoolItem item = Instantiate(_prefab, _parent).GetComponent<IPoolItem>();
            item.Pool = Pool;
            return item;
        }
    }
}