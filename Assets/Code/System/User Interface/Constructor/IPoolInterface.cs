using System.Collections.Generic;

namespace UnityEngine.Pool
{
    public interface IPoolBehaviour
    {
        public IObjectPool<IPoolItem> Pool { get; }
        public List<IPoolItem> ActiveItems { get; }
    }

    public interface IPoolItem
    {
        public int Index { get; set; }
        public Transform Object { get; }
        public bool IsActive { get; set; }
        public IObjectPool<IPoolItem> Pool { get; set; }

        public Vector2 Position { get; set; }
        public Vector2 LocalPosition { get; set; }
    }
}