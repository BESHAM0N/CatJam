using System.Collections;
using Models.PauseMenuModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace CatJam.PauseMenu
{
    public class PauseMenuController
    {
        [Inject] private ISoundService _soundService;
        [Inject] private Timer _timer;
        [Inject] private IViewAnimator _viewAnimator;

        private PauseMenuModel _model;
        private PauseMenuView _view;

        public void Initialize(PauseMenuView view, PauseMenuModel model)
        {
            _view = view;
            _model = model;
            _view.Initialize(_viewAnimator);
            _view.OnResumeClicked += HandleResume;
            _view.OnSoundToggleClicked += HandleSoundToggle;
            _view.OnExitClicked += HandleExit;

            _model.OnSoundStateChanged += _view.UpdateSoundIcon;
        }

        public void PauseGame()
        {
            _model.Pause();
            Time.timeScale = 0f;
            _view.Show(true);
        }

        private void HandleResume()
        {
            _view.Show(false);
            _model.Resume();
            Time.timeScale = 1f;
            //StartCoroutine(ResumeAfterDelay());
        }

        private IEnumerator ResumeAfterDelay()
        {
            yield return new WaitForSeconds(3f);
            _timer.ResumeTimer();
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