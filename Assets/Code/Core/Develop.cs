using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Develop : MonoBehaviour
{
    [SerializeField] private bool _isEditor;

    private void Awake()
    {
        if (_isEditor)
            PlayerPrefs.DeleteAll();
    }
}