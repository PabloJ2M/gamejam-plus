using UnityEngine;

public class SelectionPanelDefinition : UITreeNodeDefinition
{
    public string itemSetName;

    public SelectionPanelDefinition(string itemSetName)
    {
        this.itemSetName = itemSetName;        
    }
}
