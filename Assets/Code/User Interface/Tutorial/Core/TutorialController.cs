using System;
using System.Collections;
using UnityEngine.Events;

namespace UnityEngine.Tutorial
{
    [DefaultExecutionOrder(-100)]
    public class TutorialController : MonoBehaviour
    {
        [SerializeField] private TutorialContainer _container;
        [SerializeField, Range(0, 2)] private float _startDelay, _inBetweenDelay;

        [SerializeField] private UnityEvent _onComplete;

        public Action<TutorialStep> onDisplayTutorial { get; set; }
        private WaitUntil _waitForContinue;

        public static TutorialController instance;

        private void Awake() { instance = this; _waitForContinue = new(() => Continue); }
        public bool Continue { private get; set; }

        private IEnumerator Start()
        {
            if (!_container || onDisplayTutorial == null) yield break;
            yield return new WaitForSeconds(_startDelay);
            WaitForSeconds delay = new(_inBetweenDelay);

            for (int i = 0; i < _container.Count; i++)
            {
                if (_container[i] == null) continue;
                onDisplayTutorial?.Invoke(_container[i]);
                yield return _waitForContinue; Continue = false;
                yield return delay;
            }

            _onComplete.Invoke();
        }
    }
}