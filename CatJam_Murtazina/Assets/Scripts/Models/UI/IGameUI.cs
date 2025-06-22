namespace CatJam
{
    public interface IGameUI
    {
        void SetScore(string score);
        void SetTimer(string time);
        void GameOver();
    }
}