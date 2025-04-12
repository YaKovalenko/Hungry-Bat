using System.Threading.Tasks;

namespace Core.Services.SceneManagement
{
    public interface ISceneManagementService
    {
        Task LoadSceneAsync(string sceneName);
    }
}