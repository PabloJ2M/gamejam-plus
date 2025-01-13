using UnityEngine;

public class ActivateRandomObject : MonoBehaviour
{
    public GameObject[] objects;

    public void ActivateRandom()
    {
        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }

        int randomIndex = Random.Range(0, objects.Length);
        objects[randomIndex].SetActive(true);
    }
}

