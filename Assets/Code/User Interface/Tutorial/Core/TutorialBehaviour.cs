namespace UnityEngine.Tutorial
{
    public abstract class TutorialBehaviour : MonoBehaviour
    {
        protected TutorialController _controller;

        protected virtual void Awake() => _controller = TutorialController.instance;
        protected virtual void OnEnable() => _controller.onDisplayTutorial += OnPerformeTutorial;
        protected virtual void OnDisable() => _controller.onDisplayTutorial -= OnPerformeTutorial;

        protected abstract void OnPerformeTutorial(TutorialStep step);
    }
}