using System;
using CatJam.PauseMenu;
using Zenject;

namespace CatJam
{
    public sealed class PauseMenuClickObserver : IInitializable, IDisposable
    {
        private readonly ISoundService _soundService;
        private readonly PauseMenuView _view;
        
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