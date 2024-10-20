using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class CellNode : UITreeNode
{
    [SerializeField] private string guid;
    [SerializeField] private string _selectionPanelNodeName;

    [Header("Item")]
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _itemName;
    [SerializeField] private int _cost;

    void Awake()
    {
        guid = System.Guid.NewGuid().ToString();

        new List<UITreeNode>(GetComponentsInChildren<UITreeNode>()).ForEach(n => n.nodeName += guid);
    }

    public override void GetDefinitionFromContext()
    {

    }

    public override void Render()
    {
        SelectionPanel _selectionPanel = UITree.Instance.GetNode(_selectionPanelNodeName) as SelectionPanel;

        if(_selectionPanel == null) return;

        int itemNumber = (_definition as CellDefinition).itemNumber;
        Item item = _selectionPanel.GetItem(itemNumber);
        
        if(item == null) return;

        _icon = item.Icon;
        _itemName = item.Name;
        _cost = item.Cost;

        UITree.Instance.SingleOperation("item-icon"+guid, new ImageNodeDefiniton(_icon));
        UITree.Instance.SingleOperation("item-name"+guid, new TextNodeDefinition(_itemName));
        UITree.Instance.SingleOperation("item-cost"+guid, new TextNodeDefinition(_cost.ToString()));
    }
}
