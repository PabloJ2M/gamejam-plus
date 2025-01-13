namespace UnityEngine.SceneManagement
{
    public class TutorialLoader : SceneLoader
    {
        private const string SavedID = "Tutorial";

        private void Start()
        {
            if (PlayerPrefs.GetInt(SavedID, 0) != 0) return;
            PlayerPrefs.SetInt(SavedID, 1);
            AddScene();
        }
    }
}