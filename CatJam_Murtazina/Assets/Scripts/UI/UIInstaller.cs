using UnityEngine;
using Zenject;

namespace CatJam
{
    public sealed class UIInstaller : MonoInstaller
    {
        [SerializeField] private EntitiesView _entitiesView;
        [SerializeField] private Timer _timer;
        
        public override void InstallBindings()
        {
            Container.Bind<IGameUI>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesTo<EntitisPresenter>().AsSingle().NonLazy();
            ScoreInstaller.Install(Container, _entitiesView);
            TimerInstaller.Install(Container, _timer);
            
            // Container.Bind<Timer>()
            //     .FromInstance(_timer)        // <— здесь берём тот Timer, что в инспекторе
            //     .AsSingle()
            //     .NonLazy();
            //
            // Container.BindInterfacesTo<TimerUpdateObserver>()
            //     .AsSingle()
            //     .NonLazy();
            //
            // Container.BindInterfacesTo<TimerViewController>()
            //     .AsSingle()
            //     .NonLazy();
        }
    }
}