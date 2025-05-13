using UnityEngine;
using UnityEngine.Events;

public class EventDificulty : MonoBehaviour
{
    [SerializeField] private int _level = 1;
    [SerializeField] private UnityEvent _onCallback;

    private int _current;

    public void ResetDificulty() => _current = 0;
    public void AddDificulty()
    {
        _current++;
        if (_current % _level != 0) return;
        _onCallback.Invoke();
    }
}