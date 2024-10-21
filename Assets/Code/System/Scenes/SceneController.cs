using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UI.Effects;

namespace UnityEngine.SceneManagement
{
    [AddComponentMenu("System/SceneManagement/SceneController")]
    public class SceneController : SingletonBasic<SceneController>
    {
        [SerializeField] private AudioSource _source;
        [SerializeField] private FadeScene _fade;

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
            scenes.Add(value);
        }
        public void RemoveScene(string value)
        {
            if (!scenes.Contains(value)) return;
            SceneManager.UnloadSceneAsync(value, UnloadSceneOptions.None);
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