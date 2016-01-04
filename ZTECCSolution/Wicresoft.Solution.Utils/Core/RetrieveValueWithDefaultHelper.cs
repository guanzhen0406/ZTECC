using System;

namespace Wicresoft.Solution.Utils
{
    public class RetrieveValueWithDefaultHelper
    {
        public static T TryGet<T>(Func<T> tryGet, Func<T> defalutValueFunc)
        {
            var value = default(T);

            try
            {
                value = tryGet();
            }
            catch(Exception ex)
            {
                Log.Error(ex);
            }
            if (value == null && defalutValueFunc != null)
            {
                value = defalutValueFunc();
            }

            return value;
        }
    }
}
