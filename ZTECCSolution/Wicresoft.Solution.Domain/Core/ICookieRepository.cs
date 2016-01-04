using System;
using System.Web;
using StructureMap;

namespace Wicresoft.Solution.Domain
{
    [PluginFamily(IsSingleton = true)]
    public interface ICookieRepository
    {
        HttpCookie this[string key] { get; set; }

        string TryGet(string key);

        string TryGet(string key, Func<string> getDefault);

        void Set(string key, string value, int timeoutSeconds);

        void Set(string key, string value, DateTime expireTime, bool httpReadOnly = false);

        void Set(string key, string value);

        void Remove(string key);
    }
}
