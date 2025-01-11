using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IToggle
{
    public bool IsActive { get; }
    public void SetRandomValue();
}

public class ActivationGroup : MonoBehaviour
{
    [SerializeField] private UnityEvent<float> _onTogglesChanged;
    [SerializeField] private UnityEvent _onToggleCompleted;
    private List<IToggle> _toggles = new();
    private bool _isCompleted;

    private void Start() => SetRandomValues();

    [ContextMenu("Set Random")]
    public void SetRandomValues()
    {
        foreach (var toggle in _toggles) toggle.SetRandomValue();
        _isCompleted = false;
    }

    public void RegisterToggle(IToggle toggle) => _toggles.Add(toggle);
    public void UnregisterToggle(IToggle toggle) => _toggles.Remove(toggle);
    public void UpdateToggleStatus()
    {
        int amount = _toggles.Count(x => x.IsActive);
        _onTogglesChanged.Invoke((float)amount / _toggles.Count);

        if (amount != _toggles.Count || _isCompleted) return;
        _onToggleCompleted.Invoke();
        _isCompleted = true;
    }
}