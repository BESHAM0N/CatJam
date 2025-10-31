using System;
using Zenject;

namespace CatJam
{
    public class TimerUpdateObserver : IInitializable, IDisposable
    {
        private LevelManager _levelManager;
        private ITimer _timer;

        public TimerUpdateObserver(LevelManager levelManager, ITimer timer)
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
            _timer.Reset(6);
        }
    }
}