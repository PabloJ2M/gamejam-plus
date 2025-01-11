using UnityEngine.InventorySystem;
using TMPro;

namespace UnityEngine.UI
{
    public class InventoryUI_Entry : UI_Entry<Slot>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _text;

        private InventoryUI _inventory;
        public Item CurrentItem { get; private set; }

        protected override void Awake() { base.Awake(); _inventory = GetComponentInParent<InventoryUI>(); }
        private void ConvertToItem(int index) => CurrentItem = _inventory.Storage.Database.GetItemByIndex(index);

        public override void Setup(Slot data)
        {
            if (CurrentItem == null) ConvertToItem(data.ID);
            else if (CurrentItem.ID != data.ID) ConvertToItem(data.ID);

            _icon.sprite = CurrentItem.Image;
            _text?.SetText($"{data.Amount}");
        }
    }
}