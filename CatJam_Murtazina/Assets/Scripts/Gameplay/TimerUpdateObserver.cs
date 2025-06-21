using System;
using Zenject;

namespace CatJam
{
    public class TimerUpdateObserver : IInitializable, IDisposable
    {
        private LevelManager _levelManager;
        private Timer _timer;

        public TimerUpdateObserver(LevelManager levelManager, Timer timer)
        {
            _levelManager = levelManager;
            _timer = timer;
        }
        
        public void Initialize()
        {
            _levelManager.OnLevelLoading += UpdateTimers;
        }

        public void Dispose()
        {
            _levelManager.OnLevelLoading -= UpdateTimers;
        }

        private void UpdateTimers()
        {
            _timer.RestartTimer();
        }
    }
}