using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CatJam.GameOverPanel
{
    public class RestartGame : MonoBehaviour
    {
        private Button _restartButton;
        private Tween _pulseTween;

        private void Awake()
        {
            _restartButton = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartGame);

            _pulseTween = _restartButton.transform
                .DOScale(1.1f, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveAllListeners();

            if (_pulseTween != null)
            {
                _pulseTween.Kill();
                _pulseTween = null;
            }

            _restartButton.transform.localScale = Vector3.one;
        }

        private void OnRestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}