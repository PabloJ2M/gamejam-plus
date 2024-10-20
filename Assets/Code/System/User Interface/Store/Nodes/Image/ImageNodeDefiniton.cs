using UnityEngine;
using UnityEngine.UI;

public class ImageNodeDefiniton : UITreeNodeDefinition
{
    public Sprite image;

    public ImageNodeDefiniton(Sprite image)
    {
        this.image = image;
    }
}
