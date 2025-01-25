namespace CatJam
{
    public interface IGameUI
    {
        void SetScore(string score);
        void GameOver(bool win);
    }
}