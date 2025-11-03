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
            Container.BindInstance(new SceneLoadSettings(_gameSceneName)).AsSingle();
           
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
          
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<StartGameSignal>();
          
            Container.BindInterfacesAndSelfTo<StartGameFlow>().AsSingle().NonLazy();
        }
    }
}