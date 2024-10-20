using UnityEngine;

public class SelectionPanelNavigationNode : MonoBehaviour
{
    public void ChangeActiveItemSet(string itemSetName){
        UITree.Instance.TreeOperation("selection-items", new SelectionPanelDefinition(itemSetName));
    }
}
