using System.Collections;
using System.Collections.Generic;
using Inventory;
using UnityEngine;

public class SelectionPanel : UITreeNode
{
    [Header("Item Cells Pooling")]
    public CellNodePool _cellsPool;

    [System.Serializable]
    public struct ItemSet 
    {
        public string itemSetName;
        public Item[] items;
    }

    [Header("Item Set")]
    [SerializeField] private ItemSet[] _itemSets;
    private Dictionary<string, ItemSet> _itemSetsList;
    public string activeItemSet;

    void Awake()
    {
        _itemSetsList = new Dictionary<string, ItemSet>();

        for(int i = 0; i < _itemSets.Length; i++){
            ItemSet itemSet = _itemSets[i];

            _itemSetsList.Add(itemSet.itemSetName, itemSet);
        }
    }

    public override void GetDefinitionFromContext()
    {
        if(_itemSets.Length <= 0){
            _definition = new SelectionPanelDefinition(string.Empty);
            return;
        }

        activeItemSet = _itemSets[0].itemSetName;
        _definition = new SelectionPanelDefinition(activeItemSet);
    }

    public override void Render()
    {
        activeItemSet = (_definition as SelectionPanelDefinition).itemSetName;

        if(!_itemSetsList.TryGetValue(activeItemSet, out ItemSet itemSet)) {
            _cellsPool.SetActiveNodes(0);
            return;    
        }

        _cellsPool.SetActiveNodes(itemSet.items.Length);
    }

    public Item GetItem(int itemNumber){
        if(!_itemSetsList.TryGetValue(activeItemSet, out ItemSet activeSet)) return null;

        if(activeSet.items.Length <= itemNumber) return null;
        
        return _itemSetsList[activeItemSet].items[itemNumber];
    }
}
