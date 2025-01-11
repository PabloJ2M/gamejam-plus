using UnityEngine;

public class AddScoreVisualEffectSpawner : MonoBehaviour
{
    [SerializeField] GameObject effectPrefab;
    [SerializeField] RectTransform spawnPoint;

    public void SpawnVisualEffect(int amount){
        GameObject vfxGO = Instantiate(effectPrefab, spawnPoint);
        AddScoreVisualEffect vfx = vfxGO.GetComponent<AddScoreVisualEffect>();

        vfx.SetScoreAdded(amount);
    }
}
