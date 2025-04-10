using System;
using System.Threading.Tasks;

namespace Core.SceneManagement
{
    public interface ISceneManagementService
    {
        Task LoadSceneAsync(string sceneName);
    }
}