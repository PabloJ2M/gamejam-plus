using UnityEngine.UI;

namespace UnityEngine.DecorationSystem
{
    public class UI_Handler : MonoBehaviour
    {
        [SerializeField] private Manager _manager;
        private ScrollRect _rect;

        private void Awake() => _rect = GetComponent<ScrollRect>();
        public void OnDrag(ItemDec item) { _rect.vertical = false; _manager?.OnSelectItem(item); }
        public void OnDrop(bool value) { _rect.vertical = true; _manager?.OnDropItem(value); }
    }
}