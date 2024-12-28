using UnityEngine;
using Zenject;

namespace CatJam
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameObjectsView _gameObjectsView;
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
                .Bind<GameObjectsView>()
                .FromInstance(_gameObjectsView)
                .AsSingle();

            Container
                .Bind<LevelManager>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}