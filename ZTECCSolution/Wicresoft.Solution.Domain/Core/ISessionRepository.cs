using System;
using System.Web;

namespace Wicresoft.Solution.Domain
{
    public interface ISessionRepository
    {
        object this[string key] { get; set; }

        string TryGet(string key);

        T TryGet<T>(string key) where T : class;

        T TryGet<T>(string key, Func<T> getDefault) where T : class;

        void SetTimeout(int timeoutMinutes);

        void SetTimeout(HttpContext httpContext, int timeoutMinutes);

        void Remove(string key);

        void Set(string key, object value, int timeOutMinutes);
    }
}
