using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Models.MainMenu
{
    public sealed class SceneLoader : ISceneLoader
    {
        public async Task LoadAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single)
        {
            var sceneAsync = SceneManager.LoadSceneAsync(sceneName, mode);
            
            if (sceneAsync == null) 
                return;

            sceneAsync.allowSceneActivation = true;
            while (!sceneAsync.isDone)
            {
                await Task.Yield();
            }
        }
    }
}