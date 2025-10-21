using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace CatJam.PauseMenu
{
    public class PauseMenuView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.3f;
        [SerializeField] private GameObject _panel;
        [SerializeField] private GameObject _world;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _soundToggleButton;
        [SerializeField] private Button _exitButton;
        [SerializeField] private Image _soundIcon;
        [SerializeField] private Sprite _soundOnIcon;
        [SerializeField] private Sprite _soundOffIcon;

        private IViewAnimator _viewAnimator;
        private Tween _pulseTween;
        private bool _isSoundOn = true;
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
            _soundToggleButton.onClick.AddListener(ToggleSound);
            _exitButton.onClick.AddListener(() => OnExitClicked?.Invoke());
        }
        
        private void OnEnable()
        {
            _pulseTween = _resumeButton.transform
                .DOScale(1.05f, 0.7f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .Pause();
        }

        public void Show(bool active)
        {
            UpdateSoundIcon(_isSoundOn);

            if (active)
            {
                gameObject.SetActive(true);
                _world.SetActive(false);
                _pulseTween?.Play(); 
            }
            else
            {
                gameObject.SetActive(false);
                _world.SetActive(true);
                _pulseTween?.Pause();
            }
            
            // if (active)
            //     _viewAnimator.Show(_canvasGroup, _panel, _fadeDuration);
            // else
            //     _viewAnimator.Hide(_canvasGroup, _panel, _fadeDuration);
        }
        
        private void OnDestroy()
        {
            _pulseTween?.Kill();
            _soundToggleButton.onClick.RemoveListener(ToggleSound);
        }

        private void ToggleSound()
        {
            _isSoundOn = !_isSoundOn;
            UpdateSoundIcon(_isSoundOn);
            OnSoundToggleClicked?.Invoke();
        }
        
        public void UpdateSoundIcon(bool isSoundOn)
        {
            if (_soundIcon != null)
            {
                _soundIcon.sprite = isSoundOn ? _soundOnIcon : _soundOffIcon;
            }
        }
    }
}