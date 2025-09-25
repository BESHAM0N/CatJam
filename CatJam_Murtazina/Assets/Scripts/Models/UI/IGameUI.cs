using System;

namespace CatJam
{
    public interface IGameUI
    {
        event Action OnPauseClicked;
        void SetScore(string score);
        void SetTimer(string time);
        void GameOver();
    }
}