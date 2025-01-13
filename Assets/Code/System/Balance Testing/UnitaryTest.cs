using UnityEngine;
using TMPro;

public class UnitaryTest : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float timeToTest;
    [Space, SerializeField] private float testTimer;
    [SerializeField] private bool isTesting;

    private void Start() => StartTest();

    private void Update()
    {
        if(!isTesting) return;

        if(testTimer > 0) testTimer -= Time.deltaTime;
        else EndTest();
    }

    public void StartTest()
    {
        if(isTesting) return;

        isTesting = true;
        testTimer = timeToTest;
    }

    private void EndTest()
    {
        if(!isTesting) return;

        isTesting = false;

        Debug.Log("Unitary test result:" + scoreText.text);
    }
}
