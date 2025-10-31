using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CatJam
{
    public sealed class TimerView : MonoBehaviour, ITimerView
    {
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private float _scale = 1.15f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Color _pulseColor = Color.red;

        private Tween _tween;
        private Color _initialColor;
        private Vector3 _initialScale;

        private void Awake()
        {
            _initialColor = _timerText.color;
            _initialScale = _timerText.rectTransform.localScale;
        }
        
        public void SetTimeText(string text)
        {
            _timerText.text = text;
        }

        public void SetWarning(bool on)
        {
            if (on)
            {
                if (_tween != null && _tween.IsActive()) return;
                _timerText.color = _pulseColor;
                _tween = _timerText.rectTransform
                    .DOScale(_initialScale * _scale, _duration)
                    .SetEase(Ease.InOutSine)
                    .SetLoops(-1, LoopType.Yoyo);
            }
            else
            {
                if (_tween != null) { _tween.Kill(); _tween = null; }
                _timerText.rectTransform.localScale = _initialScale;
                _timerText.color = _initialColor;
            }
        }

        private void OnDisable()
        {
            SetWarning(false);
        }
    }
}