using UnityEngine;
using UnityEngine.UI;

namespace Events.Gameplay
{
    public class Image_Status : MonoBehaviour
    {
        [SerializeField] private Sprite _success, _failure;
        private Image _image;

        private void Awake() => _image = GetComponent<Image>();
        public void SetResult(bool value) => _image.sprite = value ? _success : _failure;
        public void SetDefault() => _image.sprite = _success;
    }
}