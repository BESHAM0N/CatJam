using System;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public class TimerViewController  : IInitializable, IDisposable
    {
        private Timer _timer;
        private IGameUI _gameUI;

        public TimerViewController(Timer timer, IGameUI gameUI)
        {
            _timer = timer;
            _gameUI = gameUI;
        }
        
        public void Initialize()
        {
            _timer.OnTimeUp += _gameUI.GameOver;
            _timer.OnTimeUpdated += HandleTimeUpdated;
        }

        public void Dispose()
        {
            _timer.OnTimeUp -= _gameUI.GameOver;
            _timer.OnTimeUpdated -= HandleTimeUpdated;
        }
        
        private void HandleTimeUpdated(float remainingTime)
        {
            var seconds = Mathf.FloorToInt(remainingTime);
            var hundredths = Mathf.FloorToInt((remainingTime - seconds) * 100);
            _gameUI.SetTimer($"{seconds:00}:{hundredths:00}");
        }
    }
}