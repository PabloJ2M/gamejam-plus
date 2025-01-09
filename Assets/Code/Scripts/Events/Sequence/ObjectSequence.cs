using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Unity.Mathematics;

namespace Events.Gameplay
{
    public class ObjectSequence : MonoBehaviour
    {
        [SerializeField, Range(0, 10)] private float _delay;
        [SerializeField] private GameObject[] _sequences;
        [SerializeField] private UnityEvent _onFailure;

        private int _current;
        private WaitForSeconds _wait;

        private void Awake() => _wait = new(_delay);
        private IEnumerator Delay() { yield return _wait; ObjectsStatus(_current); }
        private void ObjectsStatus(int index) { for (int i = 0; i < _sequences.Length; i++) _sequences[i].SetActive(i == index); }

        public void NextState(bool value)
        {
            if (!value) { _onFailure.Invoke(); return; }

            int length = _sequences.Length;
            _current++; _current = math.clamp(_current, 0, length);
            ObjectsStatus(-1); StartCoroutine(Delay());
        }
    }
}