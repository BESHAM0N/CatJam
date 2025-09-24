using CatJam.PauseMenu;
using Models.PauseMenuModel;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public class PauseMenuInstaller : MonoInstaller
    {
        [SerializeField] private PauseMenuView _view;

        public override void InstallBindings()
        {
            Container.Bind<PauseMenuView>().FromInstance(_view).AsSingle();
            Container.Bind<PauseMenuModel>().AsSingle();
            Container.Bind<IViewAnimator>().To<FadeViewAnimator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PauseMenuController>().AsSingle();
        }
    }
}