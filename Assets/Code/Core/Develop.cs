using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Develop : MonoBehaviour
{
    [SerializeField] private bool _isEditor;

    #if UNITY_EDITOR
    private void Awake()
    {
        if (_isEditor)
            PlayerPrefs.DeleteAll();
    }
    #endif
}