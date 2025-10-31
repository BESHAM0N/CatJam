using System;
using UnityEngine;

public class Timer
{
    public event Action OnTimeUp;
    public event Action<float> OnTimeUpdated;
    public event Action OnColorAnsSizeChanged;

    private int _timeLimitInSeconds = 5;
    private float _remainingTime;
    private bool _isTimerRunning;

    public void StartTimer()
    {
        _remainingTime = _timeLimitInSeconds;
        _isTimerRunning = true;
    }

    public void OnUpdateTimer()
    {
        if (!_isTimerRunning) return;

        _remainingTime -= Time.deltaTime;

        OnTimeUpdated?.Invoke(_remainingTime);

        if (_remainingTime <= 0)
        {
            _isTimerRunning = false;
            OnTimeUp?.Invoke();
        }

        CheckTimer();
    }

    private void CheckTimer()
    {
        if (_remainingTime <= 3)
        {
            OnColorAnsSizeChanged?.Invoke();
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

    public void ResumeTimer()
    {
        _isTimerRunning = true;
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