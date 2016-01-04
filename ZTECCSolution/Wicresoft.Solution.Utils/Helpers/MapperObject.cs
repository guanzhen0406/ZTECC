using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Wicresoft.Solution.Utils
{
    /// <summary>
    /// ObjectMapper 类负责对象之间相对应的属性间的赋值
    /// 用户Model和Entity之间赋值，属性多时用
    /// </summary>
    public class MapperObject
    {
        private static IList<PropertyMapper> GetMapperProperties(Type sourceType, Type targetType)
        {
            //去除类型，因为有的是可为NULL类型
            var sourceProperties = sourceType.GetProperties();
            var targetProperties = targetType.GetProperties();

            return (from s in sourceProperties
                    from t in targetProperties
                    where s.Name == t.Name && s.CanRead && t.CanWrite
                    //where s.Name == t.Name && s.CanRead && t.CanWrite && s.PropertyType == t.PropertyType
                    select new PropertyMapper
                    {
                        SourceProperty = s,
                        TargetProperty = t
                    }).ToList();
        }

        /// <summary>
        /// 将source赋值给target(全部赋值,包括null)
        /// </summary>
        /// <param name="source">原值对象</param>
        /// <param name="target">要被赋值的对象</param>
        public static void CopyProperties(object source, object target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();
            var mapperProperties = GetMapperProperties(sourceType, targetType);

            for (int index = 0, count = mapperProperties.Count; index < count; index++)
            {
                var property = mapperProperties[index];
                var sourceValue = property.SourceProperty.GetValue(source, null);
                //全部赋值，包括NULL
                property.TargetProperty.SetValue(target, sourceValue, null);
            }
        }

        /// <summary>
        /// 将source赋值给target(原值不为null的属性赋值)
        /// </summary>
        /// <param name="source">原值对象</param>
        /// <param name="target">要被赋值的对象</param>
        public static void CopyProperties2(object source, object target)
        {
            var sourceType = source.GetType();
            var targetType = target.GetType();
            var mapperProperties = GetMapperProperties(sourceType, targetType);

            for (int index = 0, count = mapperProperties.Count; index < count; index++)
            {
                var property = mapperProperties[index];
                var sourceValue = property.SourceProperty.GetValue(source, null);
                if (sourceValue != null)
                {
                    //原来的值不为NULL时赋值
                    property.TargetProperty.SetValue(target, sourceValue, null);
                }
            }
        }
    }

    /// <summary>
    /// 属性复制辅助类,包含源属性和目标属性
    /// </summary>
    public class PropertyMapper
    {
        /// <summary>
        /// 源属性
        /// </summary>
        public PropertyInfo SourceProperty { get; set; }

        /// <summary>
        /// 目标属性
        /// </summary>
        public PropertyInfo TargetProperty { get; set; }
    }
}
