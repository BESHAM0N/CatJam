using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action OnTimeUp;
    public event Action<float> OnTimeUpdated;

    private int _timeLimitInSeconds = 8;
    private float _remainingTime;
    private bool _isTimerRunning;

    private void Start()
    {
        _remainingTime = _timeLimitInSeconds;
        _isTimerRunning = true;
    }

    private void Update()
    {
        if (!_isTimerRunning) return;

        _remainingTime -= Time.deltaTime;

        OnTimeUpdated?.Invoke(_remainingTime);

        if (_remainingTime <= 0)
        {
            _isTimerRunning = false;
            OnTimeUp?.Invoke();
        }
    }

    public void RestartTimer()
    {
        _remainingTime = _timeLimitInSeconds;
        _isTimerRunning = true;
    }

    public void StopTimer()
    {
        _isTimerRunning = false;
    }
    
    public void AddTime(float seconds)
    {
        _remainingTime = Mathf.Clamp(_remainingTime + seconds, 0f, _timeLimitInSeconds);
        OnTimeUpdated?.Invoke(_remainingTime);

        if (_remainingTime <= 0f)
        {
            _isTimerRunning = false;
            OnTimeUp?.Invoke();
        }
    }
}
