using System.Collections.Generic;
using System.Reflection;

namespace Wicresoft.Solution.Utils
{
    public static class ClassHelper<T> where T : class
    {
        /// <summary>
        /// 比对两个实体是否相等
        /// </summary>
        /// <param name="origialModel">原Model</param>
        /// <param name="newModel">新Model</param>
        /// <returns>值不同的属性列表</returns>
        public static List<string> CompareModel(T origialModel, T newModel)
        {
            List<string> lstDifferentProperty = new List<string>();

            PropertyInfo[] piArray = typeof(T).GetProperties();
            for (int i = 0; i < piArray.Length; i++)
            {
                PropertyInfo pi = piArray[i];
                object oldValue = pi.GetValue(origialModel, null);
                object newValue = pi.GetValue(newModel, null);

                if ((oldValue == null && newValue != null && newValue.ToString() != string.Empty) || (oldValue != null && newValue == null && oldValue.ToString() != string.Empty))
                {
                    lstDifferentProperty.Add(pi.Name);
                }
                if (oldValue != null && newValue != null && oldValue.ToString() != newValue.ToString())
                {
                    lstDifferentProperty.Add(pi.Name);
                }
            }

            return lstDifferentProperty;
        }
    }
}
