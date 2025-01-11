using UnityEngine;

namespace Events.Interact
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] private GameObject _item;
        [SerializeField] private int _length;

        private Transform _transform;

        private void Awake() => _transform = transform;

        public void SpawnObject()
        {
            GameObject obj = null;
            int count = _transform.childCount;

            for (int i = 0; i < count; i++) {
                Transform child = _transform.GetChild(i);
                if (!child.gameObject.activeInHierarchy) { obj = child.gameObject; break; }
            }

            if (obj == null) {
                if (count >= _length) return;
                obj = Instantiate(_item, _transform);
            }

            obj.SetActive(true);
            obj.transform.SetPositionAndRotation(_transform.position, _transform.rotation);
        }
    }
}