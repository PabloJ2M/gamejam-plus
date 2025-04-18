namespace UnityEngine.Tutorial
{
    public class IndicatorFitter : MonoBehaviour
    {
        [SerializeField] private TutorialStep _tutorial;

        public TutorialStep Tutorial => _tutorial;
        private void OnEnable() => _tutorial.onDisplayIndicator += OnPerfomeIndicator;
        private void OnDisable() => _tutorial.onDisplayIndicator -= OnPerfomeIndicator;

        private RectTransform OnPerfomeIndicator() => GetComponent<RectTransform>();
    }
}