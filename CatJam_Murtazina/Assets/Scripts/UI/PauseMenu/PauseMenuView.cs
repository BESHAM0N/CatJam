using UnityEngine;
using UnityEngine.UI;
using System;

namespace CatJam.PauseMenu
{
    public class PauseMenuView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.3f;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _soundToggleButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Image _soundIcon;
        [SerializeField] private Sprite _soundOnIcon;
        [SerializeField] private Sprite _soundOffIcon;

        private IViewAnimator _viewAnimator;

        public event Action OnResumeClicked;
        public event Action OnSoundToggleClicked;
        public event Action OnExitClicked;

        public void Initialize(IViewAnimator viewAnimator)
        {
            _viewAnimator = viewAnimator;
        }

        private void Awake()
        {
            _resumeButton.onClick.AddListener(() => OnResumeClicked?.Invoke());
            _soundToggleButton.onClick.AddListener(() => OnSoundToggleClicked?.Invoke());
            _exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());
        }

        public void Show(bool active)
        {
            if (_viewAnimator == null)
             return;

            if (active)
                _viewAnimator.Show(_canvasGroup, _panel, _fadeDuration);
            else
                _viewAnimator.Hide(_canvasGroup, _panel, _fadeDuration);
        }

        public void UpdateSoundIcon(bool isSoundOn)
        {
            _soundIcon.sprite = isSoundOn ? _soundOnIcon : _soundOffIcon;
        }
    }
}