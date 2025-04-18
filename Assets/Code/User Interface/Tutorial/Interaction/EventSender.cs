namespace UnityEngine.Tutorial
{
    [RequireComponent(typeof(IndicatorFitter))]
    public class EventSender : MonoBehaviour
    {
        [SerializeField] private TutorialStep _tutorial;

        [ContextMenu("Interact")] public void Interact() => _tutorial.onInteractHandler?.Invoke();
    }
}