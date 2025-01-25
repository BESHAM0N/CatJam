using System;
using Zenject;

namespace CatJam
{
    public sealed class UIScoreController : IInitializable, IDisposable
    {
        private IScore _score;
        private IGameUI _gameUI;

        public UIScoreController(IScore score, IGameUI gameUI)
        {
            _score = score;
            _gameUI = gameUI;
        }
        
        public void Initialize()
        {
            _score.OnStateChanged += UpdateScore;
            StartSetScore();
        }
        
        public void Dispose()
        {
            _score.OnStateChanged -= UpdateScore;
        }
        private void UpdateScore(int score)
        {
            _gameUI.SetScore(score.ToString());
        }

        private void StartSetScore()
        {
            _gameUI.SetScore(_score.Current.ToString());
        }
    }
}