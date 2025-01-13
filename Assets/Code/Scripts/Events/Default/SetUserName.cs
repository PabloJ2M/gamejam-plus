using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SetUserName : SingletonBasic<SetUserName>
{
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text textChange;

    [SerializeField] private UnityEvent _onSuccess;

    public string username { get; private set; }

    public void ChangeText()
    {
        if (string.IsNullOrEmpty(inputField.text)) return;
        username = textChange.text = inputField.text;
        _onSuccess.Invoke();
    }
}