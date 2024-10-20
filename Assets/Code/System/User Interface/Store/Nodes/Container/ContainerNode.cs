using UnityEngine;

public class ContainerNode : UITreeNode
{
    public override void Render()
    {
        
    }

    public override void GetDefinitionFromContext()
    {
        _definition = new ContainerNodeDefinition();
    }
}
