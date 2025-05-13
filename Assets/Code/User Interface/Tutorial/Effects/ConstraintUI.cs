using System.Threading.Tasks;

namespace UnityEngine.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class ConstraintUI : MonoBehaviour
    {
        [SerializeField] private RectTransform _target;
        private RectTransform _canvasRect;

        private void Awake() => _canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        private void Start() => SetCanvasValues();
        public async void SetCanvasValues()
        {
            await Task.Yield();
            _target.position = _canvasRect.position;
            _target.sizeDelta = _canvasRect.sizeDelta;
        }
    }
}