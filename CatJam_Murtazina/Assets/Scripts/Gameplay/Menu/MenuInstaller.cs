using Models.MainMenu;
using UnityEngine;
using Zenject;

namespace CatJam.Menu
{
    public class MenuInstaller : MonoInstaller
    {
        [Header("Target scene")] 
        [SerializeField] private string _gameSceneName = "GameScene";

        public override void InstallBindings()
        {
            // Настройки
            Container.BindInstance(new SceneLoadSettings(_gameSceneName)).AsSingle();

            // Сервис загрузки
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

            // Сигналы
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<StartGameSignal>();

            // Подписчик на сигнал
            Container.BindInterfacesAndSelfTo<StartGameFlow>().AsSingle().NonLazy();
        }
    }
}