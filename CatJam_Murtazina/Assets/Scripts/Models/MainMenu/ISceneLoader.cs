using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Models.MainMenu
{
    public interface ISceneLoader
    {
        Task LoadAsync(string sceneName, LoadSceneMode mode = LoadSceneMode.Single);
    }
}