using UnityEngine.InventorySystem;
using TMPro;

namespace UnityEngine.UI
{
    public class StoreUI_Entry : UI_EntryInteract<Item>
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _cost;

        private StoreUI _store;
        private Item _item;

        protected override void Awake() { base.Awake(); _store = GetComponentInParent<StoreUI>(); }
        protected override void OnInteract() => _store?.BuyItem(_item);

        public override void Setup(Item data)
        {
            _item = data;
            _image.sprite = _item.Image;
            _cost?.SetText($"${_item.Resources}");
        }
    }
}