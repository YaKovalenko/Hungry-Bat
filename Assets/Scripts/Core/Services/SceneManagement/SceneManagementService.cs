using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Core.Services.SceneManagement
{
    public class SceneManagementService : ISceneManagementService
    {
        public async Task LoadSceneAsync(string sceneName)
        {
            var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }
        }

        public async Task UnloadSceneAsync(string sceneName)
        {
            var asyncOperation = SceneManager.UnloadSceneAsync(sceneName);
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }
        }
    }
}