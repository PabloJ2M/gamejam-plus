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
        [SerializeField] private float _threshold;
        [SerializeField] private GameObject[] _prefabs;
        [SerializeField] private Rate[] _rate;

        private int _current;

        public float Threshold => _threshold;
        public float SpawnTime => _rate[_current].Time;
        public float SpawnStep => _rate[_current].Step;
        public int GetIndex => Random.Range(0, _prefabs.Length);
        
        public void AddDificulty() => _current = math.clamp(_current + 1, 0, _rate.Length - 1);
        public void ResetDificulty() => _current = 0;

        public GameObject Create(int index) => _prefabs[index];
    }
}