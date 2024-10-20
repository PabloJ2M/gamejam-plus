using UnityEngine;

public class TextNodeDefinition : UITreeNodeDefinition
{
    public string text;

    public TextNodeDefinition(string text)
    {
        this.text = text;
    }
}
