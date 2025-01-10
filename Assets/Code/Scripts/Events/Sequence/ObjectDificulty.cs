using UnityEngine;
using UnityEngine.Pool;

namespace Gameplay.Events
{
    public class ObjectDificulty : PoolBehaviour
    {
        [SerializeField] private int _maxAmount;
        [SerializeField] private int _level = 1;

        private int _current;

        public void AddDificulty()
        {
            _current++;
            if (_current % _level != 0) return;
            if (_parent.childCount >= _maxAmount) return;
            Pool.Get();
        }
    }
}