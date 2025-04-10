using Core.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEntryPoint>();
            
            builder.Register<ISceneManagementService, SceneManagementService>(Lifetime.Singleton);
        }
    }
}