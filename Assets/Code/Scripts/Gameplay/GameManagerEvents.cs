using UnityEngine;
using UnityEngine.Events;

public class GameManagerEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent _onStart, _onStop, _onReset;
    private GameManager _manager;

    private void Awake() => _manager = GameManager.Instance;
    private void OnEnable()
    {
        _manager.onStart += _onStart.Invoke;
        _manager.onStop += _onStop.Invoke;
        _manager.onReset += _onReset.Invoke;
    }
    private void OnDisable()
    {
        _manager.onStart -= _onStart.Invoke;
        _manager.onStop -= _onStop.Invoke;
        _manager.onReset -= _onReset.Invoke;
    }

    public void OnSuccess() => _manager.OnSuccess();
    public void OnFailure() => _manager.OnFailure();
}