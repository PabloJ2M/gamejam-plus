using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBasic<GameManager>
{
    [SerializeField] private UnityEvent _onSuccess, _onFailure;

    public Action onReset, onStart, onStop;

    public void OnReset() => onReset?.Invoke();
    public void OnStart() => onStart?.Invoke();
    public void OnStop() => onStop?.Invoke();

    public void OnSuccess() => _onSuccess.Invoke();
    public void OnFailure() => _onFailure.Invoke();
}