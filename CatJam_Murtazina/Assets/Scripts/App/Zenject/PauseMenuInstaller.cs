using CatJam.PauseMenu;
using Models.PauseMenuModel;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public class PauseMenuInstaller : MonoInstaller
    {
        [SerializeField] private PauseMenuView view;

        public override void InstallBindings()
        {
            Container.Bind<PauseMenuModel>().AsSingle();
            Container.Bind<PauseMenuView>().FromInstance(view).AsSingle();
            Container.Bind<PauseMenuController>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }

        private void Start()
        {
            var controller = Container.Resolve<PauseMenuController>();
            var model = Container.Resolve<PauseMenuModel>();
            var viewInstance = Container.Resolve<PauseMenuView>();

            controller.Initialize(viewInstance, model);
        }
    }
}