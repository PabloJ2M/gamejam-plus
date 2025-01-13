using UnityEngine.Pool;

namespace UnityEngine.UI
{
    public abstract class UI_Builder : PoolBehaviour
    {
        protected virtual void OnEnable() => OnDisplay();
        protected abstract void OnDisplay();

        public override void ClearItems()
        {
            for (int i = ActiveItems.Count - 1; i >= 0; i--)
                ActiveItems[i].Pool.Release(ActiveItems[i]);
        }
        protected override void OnReleaseFromPool(IPoolItem item)
        {
            base.OnReleaseFromPool(item);
            item.Object.transform.SetAsLastSibling();
        }
    }
}