using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RandomText : MonoBehaviour
{
    public List<string> textOptions = new List<string>();
    public TextMeshProUGUI textToModify;
    void Start()
    {
        ChangeText();
    }

    public void ChangeText()
    {
        int randomIndex = Random.Range(0, textOptions.Count);
        textToModify.text = textOptions[randomIndex];
    }
}
