using UnityEngine;
using TMPro;
public class TextNode : UITreeNode
{
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public override void GetDefinitionFromContext()
    {
        if(_text == null)
        {
            _definition = new TextNodeDefinition(string.Empty);
            return;
        }

        _definition = new TextNodeDefinition(_text.text);
    }

    public override void Render()
    {
        if (_text == null) return;

        _text.text = (_definition as TextNodeDefinition).text;
    }
}
