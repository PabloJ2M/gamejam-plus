using TMPro;
using UnityEngine;

public class SetText : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text textChange;

    public string textNew;

    public static SetText Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    public void ChangeText()
    {
        string newString = inputField.text;
        textNew = inputField.text;
        textChange.text = newString;
    }
}
