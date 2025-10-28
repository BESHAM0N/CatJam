﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Models.MainMenu;
using UnityEngine.EventSystems;

namespace UI.StartGameButton
{
    [RequireComponent(typeof(Button))]
    public class StartGameButtonView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Button _button;
        [SerializeField] private float _hoverScale  = 1.1f;
        [SerializeField] private float _hoverDuration  = 0.2f;

        private SignalBus _signalBus;
        private Vector3 _originalScale;
        private Tween _scaleTween;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Reset()
        {
            _button = GetComponent<Button>();
        }

        private void Awake()
        {
            if (_button == null) 
                _button = GetComponent<Button>();
            
            _originalScale = _button.transform.localScale;
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
            _scaleTween?.Kill();
        }

        private void OnClick()
        {
            _signalBus.Fire<StartGameSignal>();
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _scaleTween?.Kill();
            _scaleTween = _button.transform
                .DOScale(_originalScale * _hoverScale, _hoverDuration)
                .SetEase(Ease.OutBack);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _scaleTween?.Kill();
            _scaleTween = _button.transform
                .DOScale(_originalScale, _hoverDuration)
                .SetEase(Ease.InOutSine);
        }
    }
}