namespace UnityEngine.Tutorial.UI
{
    [RequireComponent(typeof(WriteText))]
    public class WriteTextSkipKey : MonoBehaviour
    {
        [SerializeField] private KeyCode _key;
        private WriteText _writeText;

        private void Awake() => _writeText = GetComponent<WriteText>();
        private void Update() { if (Input.GetKeyDown(_key)) _writeText.SkipMessage(); }
    }
}