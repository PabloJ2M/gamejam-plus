namespace UnityEngine.Tutorial
{
    [CreateAssetMenu(fileName = "tutorial container", menuName = "system/tutorial/container", order = 0)]
    public class TutorialContainer : ScriptableObject
    {
        [SerializeField] private TutorialStep[] _steps;

        public TutorialStep this[int index] => _steps[index];
        public int Count => _steps.Length;
    }
}