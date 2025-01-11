namespace UnityEngine.InventorySystem
{
    [CreateAssetMenu(fileName = "Item", menuName = "System/Inventory/Item", order = 2)]
    public class Item : ScriptableObject
    {
        [SerializeField] private int _id = -1, _max = 64;
        [SerializeField] private string _name;
        [SerializeField] private Sprite _image;
        [SerializeField, Range(0, 24)] private int _resources, _research, _health;
        [SerializeField, TextArea(5, 10)] private string _description;

        private void Awake() { if (_id < 0) SetRandomID(); }
        public void SetRandomID() => _id = Mathf.Abs(GetInstanceID());

        public int ID => _id;
        public int Max => _max;
        public string Name => _name;
        public Sprite Image => _image;
        
        public int Resources => _resources;
        public int Research => _research;
        public int Health => _health;
    }
}