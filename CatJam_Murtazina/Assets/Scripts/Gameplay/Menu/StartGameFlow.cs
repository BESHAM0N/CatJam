using Models.MainMenu;
using Zenject;

namespace CatJam.Menu
{
    public sealed class StartGameFlow : IInitializable, System.IDisposable
    {
        private readonly SignalBus _bus;
        private readonly ISceneLoader _loader;
        private readonly SceneLoadSettings _settings;

        public StartGameFlow(SignalBus bus, ISceneLoader loader, SceneLoadSettings settings)
        {
            _bus = bus;
            _loader = loader;
            _settings = settings;
        }

        public void Initialize()
        {
            _bus.Subscribe<StartGameSignal>(OnStart);
        }

        public void Dispose()
        {
            _bus.Unsubscribe<StartGameSignal>(OnStart);
        }

        private async void OnStart()
        {
            await _loader.LoadAsync(_settings.GameSceneName);
        }
    }
}