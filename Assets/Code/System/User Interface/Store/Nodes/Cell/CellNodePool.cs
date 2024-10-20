using System.Collections.Generic;
using UnityEngine;

public class CellNodePool : MonoBehaviour
{
    [Header("Pool Settings")]
    [SerializeField] private List<CellNode> _cellPool;
    [SerializeField] private Transform _poolContainer;
    [SerializeField] private GameObject _cellPrefab;
    public int PoolSize => _cellPool.Count;

    private void Awake()
    {
        _cellPool = new List<CellNode>();
    }

    public void ExpandPool(int expandTo){
        if(expandTo <= PoolSize) return;

        int difference = expandTo - PoolSize;

        for(int i = 0; i < difference; i++){
            AddCellToPool();
        }

        void AddCellToPool(){
            GameObject cell = Instantiate(_cellPrefab, _poolContainer);
            CellNode cellNode = cell.GetComponent<CellNode>();

            if(cellNode == null) return;

            cellNode.SetDefinition(new CellDefinition(_cellPool.Count));
            _cellPool.Add(cellNode);    
            UITree.Instance.AttachBranch(cellNode);
        }
    }

    public CellNode GetNode(int numberOfNode){
        if(numberOfNode >= PoolSize) return null;

        return _cellPool[numberOfNode];
    }

    public void SetActiveNodes(int numberOfNodes){
        ExpandPool(numberOfNodes);

        for(int i = 0; i < PoolSize; i++){
            _cellPool[i].gameObject.SetActive(i < numberOfNodes);
        }
    }
}
