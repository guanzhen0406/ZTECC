using Wicresoft.Solution.Utils;
using System;
using System.Web;

namespace Wicresoft.Solution.Domain
{
    public class SessionRepository : ISessionRepository
    {
        public object this[string key]
        {
            get
            {
                return HttpContext.Current.Session[key];
            }
            set
            {
                HttpContext.Current.Session[key] = value;
            }
        }

        public void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }

        public string TryGet(string key)
        {
            return TryGet(key, () => string.Empty);
        }

        public T TryGet<T>(string key) where T : class
        {
            return TryGet(key, () => default(T));
        }

        public T TryGet<T>(string key, Func<T> getDefault) where T : class
        {
            var value = RetrieveValueWithDefaultHelper.TryGet<T>(() => this[key] as T, getDefault);

            return (value == null) ? default(T) : value;
        }

        public void SetTimeout(int timeoutMinutes)
        {
            HttpContext.Current.Session.Timeout = timeoutMinutes;
        }

        public void SetTimeout(HttpContext httpContext, int timeoutMinutes)
        {
            if (httpContext != null)
            {
                httpContext.Session.Timeout = timeoutMinutes;
            }
        }

        public void Set(string key, object value, int timeOutMinutes)
        {
            HttpContext httpContext = HttpContext.Current;
            httpContext.Session[key] = value;
            SetTimeout(httpContext, timeOutMinutes);
        }
    }
}

