namespace UnityEngine.SceneManagement
{
    public class TutorialLoader : SceneLoader
    {
        private const string SavedID = "Tutorial";

        public void PlayButton()
        {
            if (PlayerPrefs.HasKey(SavedID)) return;
            PlayerPrefs.SetInt(SavedID, 0);
            AddScene();
        }
    }
}