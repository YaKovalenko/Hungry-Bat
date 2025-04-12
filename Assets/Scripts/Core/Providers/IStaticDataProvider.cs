using StaticData;

namespace Core.Providers
{
    public interface IStaticDataProvider
    {
        T Get<T>() where T : StaticDataContainer;
    }
}