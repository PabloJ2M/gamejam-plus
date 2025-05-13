using Unity.Mathematics;

namespace UnityEngine.UI
{
    public class Image_Sequence : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite[] _sprites;

        private int _index;

        public void SetDefault() => _image.sprite = _sprites[0];
        public void SetImageRandom()=> _image.sprite = _sprites[Random.Range(0, _sprites.Length)];
        public void SetImageValue(float value) => _image.sprite = _sprites[math.clamp((int)(value * _sprites.Length), 0, _sprites.Length - 1)];
        public void SetNextImage()
        {
            _index++;
            _index = math.clamp(_index, 0, _sprites.Length - 1);
            _image.sprite = _sprites[_index];
        }
    }
}