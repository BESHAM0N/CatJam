using System;
using CatJam.PauseMenu;
using Zenject;

namespace CatJam
{
    public sealed class PauseMenuAndSoundClickObserver : IInitializable, IDisposable
    {
        private readonly ISoundService _soundService;
        private readonly PauseMenuView _view;

        public PauseMenuAndSoundClickObserver(ISoundService soundService, PauseMenuView view)
        {
            _soundService = soundService;
            _view = view;
        }

        public void Initialize()
        {
            _view.OnExitClicked += SoundHandler;
            _view.OnResumeClicked += SoundHandler;
            _view.OnSoundToggleClicked += SoundHandler;
        }

        public void Dispose()
        {
            _view.OnExitClicked -= SoundHandler;
            _view.OnResumeClicked -= SoundHandler;
            _view.OnSoundToggleClicked -= SoundHandler;
        }

        private void SoundHandler()
        {
            _soundService.PlaySound(SoundType.ButtonClick);
        }
    }
}