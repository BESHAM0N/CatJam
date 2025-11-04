using System;
using UnityEngine;
using Zenject;

namespace CatJam
{
    public class GameOverObserver : IInitializable, IDisposable
    {
        private readonly IScore _score;
        private readonly ITimer _timer;
        private readonly ILeaderboard _leaderboard;
        private readonly string _username;
        
        public GameOverObserver(IScore score, ILeaderboard leaderboard, ITimer timer)
        {
            _score = score;
            _leaderboard = leaderboard;
            _timer = timer;
            _username = PlayerPrefs.GetString("PlayerName", "Player");
        }
        
        public void Initialize()
        {
            _timer.OnFinished += OnGameOver;
        }

        public void Dispose()
        {
            _timer.OnFinished -= OnGameOver;
        }
        
        private void OnGameOver()
        {
            _leaderboard.UpdateUserScoreIfHigher(_username, _score.Current);
        }
    }
}