using Zenject;

namespace CatJam
{
    public class TimerInstaller : Installer<Timer, TimerInstaller>
    {
        [Inject] private Timer _timer;
        
        public override void InstallBindings()
        {
            Container.Bind<Timer>().FromInstance(_timer).AsSingle().NonLazy();
            Container.BindInterfacesTo<TimerUpdateAndCatClickController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<TimerViewController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<TimerUpdateObserver>().AsSingle().NonLazy();
        }
    }
}