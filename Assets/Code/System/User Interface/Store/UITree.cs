using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UITree : SingletonBasic<UITree>
{
    private Dictionary<string, UITreeNode> _tree;

    private void Start()
    {
        SetupTree();
    }

    private void SetupTree()
    {
        _tree = new Dictionary<string, UITreeNode>();
        UITreeNode rootNode = transform.GetComponent<UITreeNode>();

        if (rootNode == null) return;

        RecursivelyPopulateTree(rootNode);
        TreeRender(rootNode.nodeName);
    }

    private void RecursivelyPopulateTree(UITreeNode node)
    {
        _tree.Add(node.nodeName, node);
        node.GetDefinitionFromContext();

        for(int i = 0; i < node.transform.childCount; i++)
        {
            UITreeNode childNode = node.transform.GetChild(i).GetComponent<UITreeNode>();
            if (childNode == null) continue;

            node.AddChildren(childNode.nodeName);
            childNode.SetParent(node.nodeName);

            RecursivelyPopulateTree(childNode);
        }
    }

    public void AttachBranch(UITreeNode node){
        if(node == null) return;

        UITreeNode parentNode = node.transform.parent.GetComponent<UITreeNode>();
        if(parentNode == null) return;

        node.SetParent(parentNode.nodeName);
        parentNode.AddChildren(node.nodeName);
        
        RecursivelyPopulateTree(node);
    }

    public void DetachBranchFromTree(UITreeNode node)
    {
        List<string> branch = new List<string>();
        DetachBranchFromTreeStep(node, branch);

        branch.ForEach(n => _tree.Remove(n));

        void DetachBranchFromTreeStep(UITreeNode node, List<string> branch)
        {
            if (node == null) return;
            branch.Add(node.nodeName);
            UITreeNode parentNode = _tree[node.GetParent()];
            if (parentNode == null) return;
            parentNode.RemoveChild(node.nodeName);

            foreach (var childName in node.GetChildren())
            {
                UITreeNode childNode = _tree[childName];
                if (childNode == null) continue;

                DetachBranchFromTreeStep(childNode, branch);
            }
        }
    }

    public void SingleOperation(string nodeName, UITreeNodeDefinition def)
    {
        if (!_tree.TryGetValue(nodeName, out var node)) {
            Debug.LogError($"Attemp to perform signle operation in {nodeName}. Node not found");
            return;
        }

        node.SetDefinition(def);
        node.Render();
    }

    public void TreeOperation(string nodeName, UITreeNodeDefinition def)
    {
        if (!_tree.TryGetValue(nodeName, out var node)) {
            Debug.LogError($"Attemp to perform tree operation in {nodeName}. Node not found");
            return;
        }

        node.SetDefinition(def);

        TreeRender(nodeName);
    }

    public void TreeRender(string nodeName)
    {
        if (!_tree.TryGetValue(nodeName, out var node)) return;

        node.Render();

        List<string> nodeChildren = node.GetChildren();

        nodeChildren?.ForEach(c => TreeRender(c));
    }

    public UITreeNode GetNode(string nodeName)
    {
        if (!_tree.TryGetValue(nodeName, out var node)) return null;

        return node;
    }
}
