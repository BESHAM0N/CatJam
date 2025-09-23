using System;
using Zenject;

namespace CatJam
{
    public sealed class CatClickAndTimerUpdateController : IInitializable, IDisposable
    {
        private readonly EntitiesView _view;
        private readonly Timer _timer;

        public CatClickAndTimerUpdateController(EntitiesView view, Timer timer)
        {
            _view = view;
            _timer = timer;
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
            model.OnExit += () => _timer.AddTime(1f);
            model.OnDontExit += () => _timer.AddTime(-3.5f);
        }
    }
}