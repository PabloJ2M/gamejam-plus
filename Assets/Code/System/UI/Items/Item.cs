namespace UnityEngine.InventorySystem
{
    [CreateAssetMenu(fileName = "item", menuName = "system/inventory/item", order = 2)]
    public class Item : ScriptableObject
    {
        [SerializeField] protected int _id = -1, _max = 64;
        [SerializeField] protected string _name;
        [SerializeField] protected Sprite _image;
        [SerializeField, Range(0, 24)] protected int _welfare, _maintenance, _intelligence;
        [SerializeField, TextArea(5, 10)] protected string _description;

        private void Awake() { if (_id < 0) SetRandomID(); }
        [ContextMenu("Random ID")] public void SetRandomID() => _id = Mathf.Abs(GetInstanceID());

        public int ID => _id;
        public int Max => _max;
        public string Name => _name;
        public Sprite Image => _image;
        
        public int Welfare => _welfare;
        public int Maintenance => _maintenance;
        public int Intelligence => _intelligence;
    }
}