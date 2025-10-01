using System;
using Zenject;

namespace CatJam
{
    public sealed class StartLevelSoundObserver  : IInitializable, IDisposable
    {
        private readonly ISoundService _soundService;
        private readonly LevelManager _levelManager;
     
        public StartLevelSoundObserver(LevelManager levelManager, ISoundService soundService)
        {
            _levelManager = levelManager;
            _soundService = soundService;
        }

        public void Initialize()
        {
            _levelManager.OnLevelLoading += LoadingHandler;
            LoadingHandler();
        }

        public void Dispose()
        {
            _levelManager.OnLevelLoading -= LoadingHandler;
        }

        private void LoadingHandler()
        {
            _soundService.PlaySound(SoundType.BackgroundMusic);
        }
    }
}