using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace CatJam
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private EntitiesView _entitiesView;
        //[SerializeField] private Timer _timer;
        private readonly Vector2Int _groundSize = new(5, 5);

        public override void InstallBindings()
        {
            Container
                .Bind<Ground>()
                .AsSingle()
                .WithArguments(_groundSize)
                .NonLazy();

            Container
                .Bind<EntityFactory>()
                .AsSingle()
                .WithArguments(Container.Resolve<Ground>());
            
            Container
                .Bind<EntitiesView>()
                .FromInstance(_entitiesView)
                .AsSingle();

            Container
                .Bind<LevelManager>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();            
        }
    }
}