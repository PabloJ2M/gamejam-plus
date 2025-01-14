using System;
using System.Collections;
using System.Collections.Generic;
using UI.Effects;
using UnityEngine.Events;

namespace UnityEngine.SceneManagement
{
    [AddComponentMenu("System/SceneManagement/SceneController")]
    public class SceneController : SingletonBasic<SceneController>
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private FadeScene _fade;

        [SerializeField] private UnityEvent<bool> _onSceneOverlap;

        public List<string> scenes { get; protected set; }
        public Action onSwitchScene;
        private bool _lock;

        protected override void Awake() { base.Awake(); scenes = new(); }
        public void CutScene(string value) => SceneManager.LoadScene(value);
        public void SwipeScene(string value) => OnFading(value);
        public void Quit() => OnFading(string.Empty);

        public IEnumerator AddScene(string value)
        {
            if (scenes.Contains(value)) yield break;
            yield return SceneManager.LoadSceneAsync(value, LoadSceneMode.Additive);
            _onSceneOverlap.Invoke(false);
            scenes.Add(value);
        }
        public void RemoveScene(string value)
        {
            if (!scenes.Contains(value)) return;
            SceneManager.UnloadSceneAsync(value, UnloadSceneOptions.None);
            _onSceneOverlap.Invoke(true);
            scenes.Remove(value);
        }
        private void OnFading(string value)
        {
            if (_lock) return; else _lock = true; onSwitchScene?.Invoke();
            FadeScene fade = Instantiate(_fade, transform);
            fade.onComplete += onComplete;
            fade.onUpdate += onUpdate;

            void onUpdate(float value) { if (_source) _source.volume = 1 - value; }
            void onComplete()
            {
                if (string.IsNullOrEmpty(value)) Application.Quit();
                else SceneManager.LoadSceneAsync(value, LoadSceneMode.Single);
            }
        }
    }
}