using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Events;
using Unity.Mathematics;

namespace Gameplay.Runner
{
    public class Spawner : PoolCore
    {
        [SerializeField] private Transform _parent;
        [SerializeField] private SpawnRate _spawnRate;
        [SerializeField] private UnityEvent<float> _onSpeedChanged;
        [SerializeField] private UnityEvent _onCollectScore;

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
            IPoolItem obstacle = Create(_spawnRate.CreateObstacle(_index), _index, float2.zero);
            if (_spawnRate.DropItem) Create(_spawnRate.CreateItem(), -1, mathf.up * 3.5f);
            obstacle.Index = _index;
            return obstacle;
        }

        private IPoolItem Create(GameObject item, int id, float2 offset)
        {
            int index = _pool.FindIndex(x => !x.IsActive && x.Index == id);

            IPoolItem obj = null;
            if (index >= 0) { obj = _pool[index]; obj.IsActive = true; }
            else { obj = Instantiate(item, _parent).GetComponent<IPoolItem>(); _pool.Add(obj); }

            ActiveItems.Add(obj);
            obj.LocalPosition = mathf.zero + offset;
            return obj;
        }
        public void OnCollectScore() => _onCollectScore.Invoke();
    }
}