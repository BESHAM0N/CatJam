using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CatJam
{
    public class GameUI : MonoBehaviour, IGameUI
    {
        public event Action OnPauseClicked;
        
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private GameObject _world;
        
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private TMP_Text _gameOverScoreText;
        
        [SerializeField] private Button _pauseButton;

        private void Awake()
        {
            StartGame();
        }

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(() => OnPauseClicked?.Invoke());
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveAllListeners();
        }

        private void StartGame()
        {
            _gameOverScreen.SetActive(false);
            _world.SetActive(true);
            _score.gameObject.SetActive(true);
            _timerText.gameObject.SetActive(true);
        }

        public void SetScore(string score)
        {
            _score.text = $"Score: {score}";
        }
        
        public void SetTimer(string  text)
        {
            _timerText.text = text;
        }

        public void GameOver()
        {
            _gameOverScreen.SetActive(true);
            _world.SetActive(false);
            _score.gameObject.SetActive(false);
            _timerText.gameObject.SetActive(false);
            SetGameOverScoreText();
        }

        private void SetGameOverScoreText()
        {
            _gameOverScoreText.text = $"You helped me free {_score.text} cats. Shall we continue?";
        }
    }
}