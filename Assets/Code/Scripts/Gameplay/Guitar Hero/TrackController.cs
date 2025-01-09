using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.GuitarHero
{
    public class TrackController : PoolBehaviour
    {
        [SerializeField] private BeatDataScriptable _source;
        [SerializeField] private float _pixelSpeedPerSecond;

        [SerializeField] private UnityEvent _onSuccess, _onFailure;
        [SerializeField] private UnityEvent _onComplete;

        public float Limit => -_parent.rect.height;
        private int RandomIndex => Random.Range(0, _parent.childCount);

        private bool _isCompleted;
        private float _offset;
        private float _time;
        private int _index;

        private void Start()
        {
            _offset = (_parent.rect.height / (_pixelSpeedPerSecond * Time.fixedDeltaTime)) * Time.fixedDeltaTime;
            _time = _offset;
        }
        private void Update()
        {
            if (_source.Count == 0 || _isCompleted) return;
            _time += Time.deltaTime;

            if (_time - _offset >= _source.Length) { _isCompleted = true; _onComplete.Invoke(); return; }
            if (_index >= _source.Count || _time < _source.BeatTimes[_index]) return;

            Pool.Get(); _index++;
        }
        private void FixedUpdate()
        {
            foreach (var item in ActiveItems)
                item.LocalPosition += _pixelSpeedPerSecond * Time.fixedDeltaTime * (Vector2)mathf.down;
        }

        protected override IPoolItem CreateItem()
        {
            IPoolItem item = Instantiate(_prefab, _parent.GetChild(RandomIndex)).GetComponent<IPoolItem>();
            item.LocalPosition = mathf.zero;
            item.Pool = Pool;
            return item;
        }
        protected override void OnGetFromPool(IPoolItem item)
        {
            item.Object.transform.SetParent(_parent.GetChild(RandomIndex));
            item.LocalPosition = mathf.zero;
            base.OnGetFromPool(item);

            TrackItem o = item as TrackItem;
            o.OnGet();
        }
        protected override void OnReleaseFromPool(IPoolItem item)
        {
            TrackItem o = item as TrackItem;
            o.OnRelease();

            base.OnReleaseFromPool(item);
        }

        public void MissedTrack() => _onFailure.Invoke();
        public void SuccessTrack() => _onSuccess.Invoke();
    }
}