using Zenject;

namespace CatJam
{
    public sealed class ScoreInstaller : Installer<EntitiesView, ScoreInstaller>
    {
        [Inject] private EntitiesView _view;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<Score>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<UIScoreController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScoreIncreaseObserver>().AsSingle().WithArguments(_view).NonLazy();
        }
    }
}