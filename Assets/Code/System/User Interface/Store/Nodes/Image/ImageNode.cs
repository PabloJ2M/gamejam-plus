using UnityEngine;
using UnityEngine.UI;

public class ImageNode : UITreeNode
{
    private Image _img;

    private void Awake()
    {
        _img = GetComponent<Image>();
    }

    public override void GetDefinitionFromContext()
    {
        if (_img == null)
        {
            _definition = new ImageNodeDefiniton(null);
            return;
        }

        _definition = new ImageNodeDefiniton(_img.sprite);
    }

    public override void Render()
    {
        if (_img == null) return;

        _img.sprite = (_definition as ImageNodeDefiniton).image;
    }
}
