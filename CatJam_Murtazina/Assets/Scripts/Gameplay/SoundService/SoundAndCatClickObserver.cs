using System;
using Zenject;

namespace CatJam
{
    public class SoundAndCatClickObserver : IInitializable, IDisposable
    {
        private readonly ISoundService _soundService;
        private readonly EntitiesView _view;

        public SoundAndCatClickObserver(EntitiesView view, ISoundService soundService)
        {
            _view = view;
            _soundService = soundService;
        }

        public void Initialize()
        { 
            _view.OnCatViewCreated += HandleCatView;
        }

        public void Dispose()
        {
            _view.OnCatViewCreated -= HandleCatView;
        }
        
        private void HandleCatView(Cat model, ClickableObject clickable)
        {
            model.OnExit += () => _soundService.PlaySound(SoundType.CatClickTrue);
            model.OnDontExit += () => _soundService.PlaySound(SoundType.CatClickFalse);
        }
    }
}