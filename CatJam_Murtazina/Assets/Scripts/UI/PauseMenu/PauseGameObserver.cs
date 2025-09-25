using System;
using Zenject;

namespace CatJam.PauseMenu
{
    public class PauseGameObserver : IInitializable, IDisposable
    {
        private readonly PauseMenuController _pauseMenuController;
        private readonly IGameUI _gameUI;

        public PauseGameObserver(PauseMenuController pauseMenuController, IGameUI gameUI)
        {
            _pauseMenuController = pauseMenuController;
            _gameUI = gameUI;
        }

        public void Initialize()
        {
            _gameUI.OnPauseClicked += _pauseMenuController.PauseGame;
        }

        public void Dispose()
        {
            _gameUI.OnPauseClicked -= _pauseMenuController.PauseGame;
        }
    }
}