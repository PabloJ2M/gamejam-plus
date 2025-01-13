using UnityEngine.UI.Display;
using UnityEngine.InventorySystem;
using TMPro;

namespace UnityEngine.UI
{
    public class StoreUI_Entry : UI_Entry<Item>
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private DisplayUI _welfare, _maintenance, _inteligence;

        private StoreUI _store;
        private Item _item;

        protected override void Awake() { base.Awake(); _store = GetComponentInParent<StoreUI>(); }
        public void OnInteract() => _store?.BuyItem(_item);

        public override void Setup(Item data)
        {
            _item = data;
            _name?.SetText(_item.Name);
            _image.sprite = _item.Image;
            _welfare.SetActive(data.Welfare > 0); _welfare.SetText(data.Welfare);
            _maintenance.SetActive(data.Maintenance > 0); _maintenance.SetText(data.Maintenance);
            _inteligence.SetActive(data.Intelligence > 0); _inteligence.SetText(data.Intelligence);
        }
    }
}