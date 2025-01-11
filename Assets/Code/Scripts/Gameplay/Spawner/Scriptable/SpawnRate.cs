using System;
using Unity.Mathematics;

namespace UnityEngine.Pool
{
    [Serializable] public struct Rate
    {
        [SerializeField] private float _min, _max;
        [SerializeField] private float _step;

        public float Time => Random.Range(_min, _max);
        public float Step => _step;
    }

    [CreateAssetMenu(fileName = "spawn rate", menuName = "system/pooling/spawn rate")]
    public class SpawnRate : ScriptableObject
    {
        [SerializeField, Range(0, 1)] private float _probability;
        [SerializeField] private GameObject[] _items;

        [SerializeField] private float _threshold;
        [SerializeField] private GameObject[] _obstacles;
        [SerializeField] private Rate[] _rate;

        private int _current;

        public float Threshold => _threshold;
        public float SpawnTime => _rate[_current].Time;
        public float SpawnStep => _rate[_current].Step;
        public int GetIndex => Random.Range(0, _obstacles.Length);
        public bool DropItem => Random.value <= _probability;
        
        public void AddDificulty() => _current = math.clamp(_current + 1, 0, _rate.Length - 1);
        public void ResetDificulty() => _current = 0;

        public GameObject CreateObstacle(int index) => _obstacles[index];
        public GameObject CreateItem() => _items[Random.Range(0, _items.Length)];
    }
}