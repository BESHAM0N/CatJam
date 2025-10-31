using System;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public class TimerViewController  : IInitializable, IDisposable
    {
        private readonly ITimer _timer;
        private readonly ITimerView _view;
        private readonly IGameUI _gameUI;

        public TimerViewController(ITimer timer, ITimerView view, IGameUI gameUI)
        {
            _timer = timer;
            _view = view;
            _gameUI = gameUI;
        }
        
        public void Initialize()
        {
            _timer.OnTick += HandleTick;
            _timer.OnFinished += HandleFinished;
            _timer.OnWarning += HandleWarning;
        }

        public void Dispose()
        {
            _timer.OnTick -= HandleTick;
            _timer.OnFinished -= HandleFinished;
            _timer.OnWarning -= HandleWarning;
        }
        
        private void HandleTick(float remaining)
        {
            _view.SetTimeText(FormatMmSs(remaining));
        }

        private void HandleFinished() => _gameUI.GameOver();

        private void HandleWarning(bool on) => _view.SetWarning(on);

        private static string FormatMmSs(float remaining)
        {
            int totalMs = Mathf.Max(0, Mathf.RoundToInt(remaining * 1000f));
            int seconds = totalMs / 1000;
            int centis = (totalMs % 1000) / 10;
            return $"{seconds:00}:{centis:00}";
        }
    }
}