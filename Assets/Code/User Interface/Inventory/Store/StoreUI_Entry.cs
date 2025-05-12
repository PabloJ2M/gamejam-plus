using UnityEngine.InventorySystem;
using TMPro;

namespace UnityEngine.UI
{
    public class StoreUI_Entry : UI_Entry<Item>
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _name;

        [Header("Cost")]
        [SerializeField] private Image _costIcon;
        [SerializeField] private TextMeshProUGUI _costText;
        [SerializeField] private Sprite _coin, _heart;

        private StoreUI _store;
        private Item _item;

        protected override void Awake() { base.Awake(); _store = GetComponentInParent<StoreUI>(); }
        public void OnInteract() => _store?.BuyItem(_item);

        public override void Setup(Item data)
        {
            _item = data;
            _image.sprite = _item.Image;
            _name?.SetText(_item.Name);

            bool isHearts = data.Hearts > 0;
            _costIcon.sprite = isHearts ? _heart : _coin;
            _costText.SetText(isHearts ? data.Hearts.ToString() : data.Coins.ToString());
        }
    }
}