using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;

namespace Gameplay.Runner
{
    public class Spawner : PoolCore
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private SpawnRate _spawnRate;
        [SerializeField] private UnityEvent<float> _onSpeedChanged;

        private List<IPoolItem> _pool = new();

        public float Limit => -_parent.position.x;
        private float _speed = 1, _time;
        private int _index;

        private void Awake() => _spawnRate.ResetDificulty();
        private void OnEnable() => _onSpeedChanged.Invoke(_speed);

        private IEnumerator Start()
        {
            float delay = _spawnRate.SpawnTime;
            yield return new WaitForSeconds(delay);
            if (_time >= _spawnRate.Threshold) { _spawnRate.AddDificulty(); _time %= _spawnRate.Threshold; }

            _index = _spawnRate.GetIndex;
            _time += delay;
            
            CreateItem();
            StartCoroutine(Start());
        }
        private void Update()
        {
            if (_speed == _spawnRate.SpawnStep) return;
            _speed = Mathf.MoveTowards(_speed, _spawnRate.SpawnStep, 0.1f * Time.deltaTime);
            _onSpeedChanged.Invoke(_speed);
        }

        public void Release(IPoolItem item) => OnReleaseFromPool(item);
        protected override IPoolItem CreateItem()
        {
            int index = _pool.FindIndex(x => !x.IsActive && x.Index == _index);

            IPoolItem item = null;
            if (index >= 0) { item = _pool[index]; item.IsActive = true; }
            else { item = Instantiate(_spawnRate.Create(_index), _parent).GetComponent<IPoolItem>(); _pool.Add(item); }

            ActiveItems.Add(item);
            item.LocalPosition = mathf.zero;
            item.Index = _index;
            return item;
        }
    }
}