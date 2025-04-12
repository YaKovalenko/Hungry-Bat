using System;
using System.Collections.Generic;
using System.Linq;
using StaticData;

namespace Core.Providers
{
    public class StaticDataProvider : IStaticDataProvider
    {
        private readonly Dictionary<Type, StaticDataContainer> _staticDataContainers;

        public StaticDataProvider(StaticDataContainer[] staticDataContainers)
        {
            _staticDataContainers = staticDataContainers.ToDictionary(x => x.GetType(), y => y);
        }

        public T Get<T>() where T : StaticDataContainer
        {
            return (T)_staticDataContainers[typeof(T)];
        }
    }
}