using UnityEngine.Pool;

namespace UnityEngine.UI
{
    public abstract class UI_Builder : PoolBehaviour
    {
        protected virtual void OnEnable() => OnDisplay();
        protected abstract void OnDisplay();

        protected override void OnReleaseFromPool(IPoolItem item)
        {
            base.OnReleaseFromPool(item);
            item.Object.transform.SetAsLastSibling();
        }
        protected void OnRemoveExceed(int index)
        {
            for (int i = ActiveItems.Count - 1; i >= index; i--)
                ActiveItems[i].Pool.Release(ActiveItems[i]);
        }
    }
}