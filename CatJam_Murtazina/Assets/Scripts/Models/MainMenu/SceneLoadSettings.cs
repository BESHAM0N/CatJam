namespace Models.MainMenu
{
    public class SceneLoadSettings
    {
        public string GameSceneName { get; }

        public SceneLoadSettings(string gameSceneName)
        {
            GameSceneName = gameSceneName;
        }
    }
}