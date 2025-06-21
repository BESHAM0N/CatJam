using System;
using Zenject;

namespace CatJam
{
    public class TimerViewObserver  : IInitializable, IDisposable
    {
        private Timer _timer;
        private IGameUI _gameUI;

        public TimerViewObserver(Timer timer, IGameUI gameUI)
        {
            _timer = timer;
            _gameUI = gameUI;
        }
        
        public void Initialize()
        {
            _timer.OnTimeUp += _gameUI.GameOver;
            _timer.OnTimeUpdated += _gameUI.SetTimer;
        }

        public void Dispose()
        {
            _timer.OnTimeUp -= _gameUI.GameOver;
            _timer.OnTimeUpdated -= _gameUI.SetTimer;
        }
    }
}