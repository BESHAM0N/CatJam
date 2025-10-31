using Zenject;

namespace CatJam
{
    public class TimerInstaller : Installer<TimerView, TimerInstaller>
    {
       [Inject] private TimerView _timerView;
        
        public override void InstallBindings()
        {
            Container.Bind<ITimer>().To<CountdownTimer>().AsSingle();
            Container.Bind<ITimerView>().FromInstance(_timerView).AsSingle();
            Container.BindInterfacesTo<TimerViewController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<TimerUpdateAndCatClickController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<TimerUpdateObserver>().AsSingle().NonLazy();
        }
    }
}