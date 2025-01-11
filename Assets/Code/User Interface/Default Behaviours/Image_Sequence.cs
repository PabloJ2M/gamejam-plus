using Unity.Mathematics;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(Image))]
    public class Image_Sequence : MonoBehaviour
    {
        [SerializeField] private Sprite[] _sprites;

        private Image _image;
        private int _index;

        private void Awake() => _image = GetComponent<Image>();
        public void SetImageRandom()=> _image.sprite = _sprites[Random.Range(0, _sprites.Length)];
        public void SetNextImage()
        {
            _index++;
            _index = math.clamp(_index, 0, _sprites.Length - 1);
            _image.sprite = _sprites[_index];
        }
    }
}