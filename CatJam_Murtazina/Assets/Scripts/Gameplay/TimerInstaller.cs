using Zenject;

namespace CatJam
{
    // public class TimerInstaller : Installer<Timer, TimerInstaller>
    // {
    //     [Inject]
    //     private Timer _timer;
    //     
    //     public override void InstallBindings()
    //     {
    //         Container.BindInterfacesAndSelfTo<Timer>().AsSingle().NonLazy();
    //         Container.BindInterfacesAndSelfTo<UIScoreController>().AsSingle().NonLazy();
    //         Container.BindInterfacesAndSelfTo<ScoreIncreaseObserver>().AsSingle().WithArguments(_view).NonLazy();
    //     }
    // }
}