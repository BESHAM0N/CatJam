namespace CatJam
{
    public interface IGameUI
    {
        void SetScore(string score);
        void SetTimer(int time);
        void GameOver();
    }
}