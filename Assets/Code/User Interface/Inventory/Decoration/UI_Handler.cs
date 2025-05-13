using UI.Effects;
using UnityEngine.UI;

namespace UnityEngine.DecorationSystem
{
    public class UI_Handler : MonoBehaviour
    {
        [SerializeField] private Manager _manager;
        [SerializeField] private Image _preview;
        [SerializeField] private FadeCanvas _canvas;

        private ScrollRect _rect;

        private void Awake() => _rect = GetComponent<ScrollRect>();

        public void OnPreview(ItemDec item)
        {
            if (!_preview) return;
            _preview.enabled = item.Image != null;
            _preview.sprite = item.Image;
        }
        public void OnDrag(ItemDec item)
        {
            _canvas.FadeIn();
            _rect.vertical = false;
            _manager?.OnSelectItem(item);
        }
        public void OnDrop(bool value)
        {
            _canvas.FadeOut();
            _rect.vertical = true;
            _manager?.OnDropItem(value);
        }
    }
}