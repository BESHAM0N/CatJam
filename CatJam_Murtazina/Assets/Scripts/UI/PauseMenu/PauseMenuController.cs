using System;
using Models.PauseMenuModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CatJam.PauseMenu
{
    public class PauseMenuController : IInitializable, IDisposable
    {
        private readonly ISoundService _soundService;
        private readonly IViewAnimator _viewAnimator;
        private readonly PauseMenuModel _model;
        private readonly PauseMenuView _view;

        public PauseMenuController(ISoundService soundService, IViewAnimator viewAnimator, PauseMenuModel model, PauseMenuView view)
        {
            _soundService = soundService;
            _viewAnimator = viewAnimator;
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            _view.Initialize(_viewAnimator);
            _view.OnResumeClicked += HandleResume;
            _view.OnSoundToggleClicked += HandleSoundToggle;
            _view.OnExitClicked += HandleExit;
            _model.OnSoundStateChanged += _view.UpdateSoundIcon;

            _view.Show(false);
        }
        
        public void Dispose()
        {
            _view.OnResumeClicked -= HandleResume;
            _view.OnSoundToggleClicked -= HandleSoundToggle;
            _view.OnExitClicked -= HandleExit;
            _model.OnSoundStateChanged -= _view.UpdateSoundIcon;
        }

        public void PauseGame()
        {
            if (_model.IsPaused) return;
            _model.Pause();
            Time.timeScale = 0f;
            _view.Show(true);
        }

        private void HandleResume()
        {
            
            if (!_model.IsPaused) return;
            _view.Show(false);
            _model.Resume();
            Time.timeScale = 1f;
        }

        private void HandleSoundToggle()
        {
            _model.ToggleSound();
            _soundService.ToggleSound(_model.IsSoundEnabled);
        }

        private void HandleExit()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu");
        }
    }
}