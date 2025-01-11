namespace UnityEngine.InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "System/Inventory/Item", order = 2)]
    public class Item : ScriptableObject
    {
        [SerializeField] private int _id = -1, _max = 64;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;
        [SerializeField] private int _cost = 0;
        [SerializeField, TextArea(5, 10)] private string _description;

        private void Awake() { if (_id < 0) SetRandomID(); }
        public void SetRandomID() => _id = Mathf.Abs(GetInstanceID());

        public int ID => _id;
        public int Max => _max;
        public int Cost => _cost;
        public string Name => _name;
        public Sprite Image => _image;
    }
}