using UnityEngine;
using Zenject;

namespace CatJam
{
    public sealed class UIInstaller : MonoInstaller
    {
        [SerializeField] private EntitiesView _entitiesView;
        [SerializeField] private TimerView _timer;
        [SerializeField] private PauseMenuView _view;
        
        public override void InstallBindings()
        {
            Container.Bind<IGameUI>().FromComponentInHierarchy().AsSingle().NonLazy();
            Container.BindInterfacesTo<EntitisPresenter>().AsSingle().NonLazy();
            ScoreInstaller.Install(Container, _entitiesView);
            TimerInstaller.Install(Container, _timer);
            PauseMenuInstaller.Install(Container, _view);
            Container.BindInterfacesAndSelfTo<PauseGameObserver>().AsSingle().NonLazy();
        }
    }
}