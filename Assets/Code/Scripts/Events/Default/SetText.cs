using TMPro;
using UnityEngine;

public class SetText : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text textChange;

    public void ChangeText()
    {
        string newString = inputField.text;
        textChange.text = newString;
    }
}
