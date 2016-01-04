using System;
using StructureMap;

namespace Wicresoft.Solution.Domain
{
    [PluginFamily(IsSingleton = true)]
    public interface IScreenAdapter
    {
        void SetViewport(Action<string> setViewport);
    }
}
