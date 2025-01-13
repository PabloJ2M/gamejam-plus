using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UI.Effects;

namespace Gameplay.Movement
{
    public class Blink : Fade
    {
        private Image _image;
        [SerializeField] private UnityEvent _onUnseen;

        private void Awake() => _image = GetComponent<Image>();

        protected override void OnUpdate(float value)
        {
            base.OnUpdate(value);
            _image.Fade(value);
        }
        protected override void OnComplete()
        {
            base.OnComplete();
            _onUnseen.Invoke();
            if (_isVisible) FadeOut();
        }
    }
}