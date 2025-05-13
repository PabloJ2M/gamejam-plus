using UnityEngine;
using UnityEngine.UI.Display;

namespace Player.Data
{
    public class ResourceSender : MonoBehaviour
    {
        [SerializeField] private Score scoreManager;
        [SerializeField] private ResourceType[] resourcesToSave;

        public void SaveResources()
        {
            for(int i = 0; i < resourcesToSave.Length; i++)
                ResourceManager.AddResource(resourcesToSave[i], scoreManager.score);
        }
    }
}