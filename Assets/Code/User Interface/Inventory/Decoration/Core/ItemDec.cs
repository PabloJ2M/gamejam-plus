using UnityEngine.InventorySystem;

namespace UnityEngine.DecorationSystem
{
    [CreateAssetMenu(fileName = "Decoration", menuName = "System/Decoration/Decoration", order = 1)]
    public class ItemDec : Item
    {
        [SerializeField] private Sprite _back, _left, _right;

        public Sprite GetDirection(int index)
        {
            switch (index % 4) {
                case 1: return _right;
                case 2: return _left;
                case 3: return _back;
                default: return Image;
            }
        }
    }
}