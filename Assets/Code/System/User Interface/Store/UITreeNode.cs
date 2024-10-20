using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public abstract class UITreeNode : MonoBehaviour
{
    public string nodeName;
    protected UITreeNodeDefinition _definition;

    private string _parentNode;
    private List<string> _children;

    public abstract void GetDefinitionFromContext();
    public abstract void Render();
    public UITreeNodeDefinition GetDefinition() => _definition;
    public void SetDefinition(UITreeNodeDefinition def) {
        if(_definition == null) {
            _definition = def;
            return;
        }
        if (def.GetType() != _definition.GetType()) return;

        _definition = def;
    }

    public void SetParent(string parentNode) => this._parentNode = parentNode;
    public void AddChildren(string child)
    {
        if(_children == null) _children = new List<string>();

        _children.Add(child);
    }

    public void RemoveChild(string child)
    {
        if (_children == null) return;
        
        if(_children.Contains(child)) _children.Remove(child);
    }
    public string GetParent() => _parentNode;
    public List<string> GetChildren() => _children;
}
