using UnityEngine;

public class OnDeathTimer : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private bool end;

    private void Update()
    {
        if(end) return;

        timer += Time.deltaTime;
    }

    public void End()
    {
        end = true;
        Debug.Log($"Game end in {timer} seconds");
    }
}
