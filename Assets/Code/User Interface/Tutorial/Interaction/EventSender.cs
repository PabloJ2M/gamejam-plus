namespace UnityEngine.Tutorial
{
    [RequireComponent(typeof(IndicatorFitter))]
    public class EventSender : MonoBehaviour
    {
        [SerializeField] private TutorialStep _tutorial;
        [SerializeField] private GameObject _animation;

        private void Start() => SetStatus(false);
        private void OnEnable() => _tutorial.onDisplayAnimation += PerformeAnimation;
        private void OnDisable() => _tutorial.onDisplayAnimation -= PerformeAnimation;

        private void PerformeAnimation() => SetStatus(true);
        private void SetStatus(bool value) { if (_animation) _animation.SetActive(value); }
        
        [ContextMenu("Interact")] public void Interact()
        {
            _tutorial.onInteractHandler?.Invoke();
            SetStatus(false);
        }
    }
}