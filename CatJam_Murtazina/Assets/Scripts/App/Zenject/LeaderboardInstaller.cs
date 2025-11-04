using Zenject;

namespace CatJam
{
    public class LeaderboardInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ILeaderboard>().To<Leaderboard>().AsSingle().NonLazy();
        }
    }
}