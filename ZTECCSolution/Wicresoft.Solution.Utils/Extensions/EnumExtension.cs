using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading;

namespace Wicresoft.Solution.Utils
{
    /// <summary>
    /// public enum RequestType   
    /// {
    ///     [EnumItemDescription("zh-CN","参观")]
    ///     [EnumItemDescription("en-US", "Visit")]
    ///     Visit = 1,
    ///     [EnumItemDescription("zh-CN", "场地租借")]
    ///     [EnumItemDescription("en-US", "SiteBorrow")]
    ///     SiteBorrow = 2,
    ///     [EnumItemDescription("zh-CN", "测试")]
    ///     [EnumItemDescription("en-US", "Testing")]
    ///     Testing = 3
    /// }
    /// 
    /// ddlRequestType.Items.Clear();
    /// ddlRequestType.Items.Add(new ListItem(Resources.CommonResource.PleaseSelect));
    /// ddlRequestType.DataSource = new EnumDataSource<RequestType>();
    /// ddlRequestType.DataTextField = "DisplayValue";
    /// ddlRequestType.DataValueField = "enumValue";
    /// ddlRequestType.DataBind();
    /// </summary>
    /// <typeparam name="EnumType"></typeparam>
    public class EnumDataSource<EnumType> : List<EnumAdapter<EnumType>>
    {
        public EnumDataSource()
            : this(false)
        { }

        public EnumDataSource(bool isFilter, int enumFilter = 0)
        {
            if (!typeof(EnumType).IsEnum)
            {
                throw new NotSupportedException("Can not support type: " + typeof(EnumType).FullName);
            }

            foreach (var value in Enum.GetValues(typeof(EnumType)))
            {
                if (isFilter && (int)value == enumFilter)
                    continue;
                base.Add(new EnumAdapter<EnumType>((int)value));
            }
        }

        public static string GetDisplayValue(int value, int culture)
        {
            return GetDisplayValue(value, CultureInfo.GetCultureInfo(culture));
        }

        public static string GetDisplayValue(int value, string culture)
        {
            return GetDisplayValue(value, CultureInfo.GetCultureInfo(culture));
        }

        private static string GetDisplayValue(int value, CultureInfo culture)
        {
            return EnumItemDescription.GetDisplayValue(value, typeof(EnumType), culture);
        }
    }

    public sealed class EnumAdapter<EnumType>
    {
        public EnumAdapter(int value)
        {
            if (!Enum.IsDefined(typeof(EnumType), value))
            {
                throw new ArgumentException(
                    string.Format("{0} is not defined in {1}",
                    value, typeof(EnumType).Name), "value");
            }
            EnumValue = value;
        }

        public int EnumValue
        {
            get;
            private set;
        }

        public string DisplayValue
        {
            get
            {
                return EnumItemDescription.GetDisplayValue(EnumValue,
                    typeof(EnumType),
                    Thread.CurrentThread.CurrentUICulture);
            }
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumItemDescription : Attribute
    {
        private static readonly Dictionary<Type, Dictionary<int, Dictionary<CultureInfo, EnumItemDescription>>> _cache =
            new Dictionary<Type, Dictionary<int, Dictionary<CultureInfo, EnumItemDescription>>>();

        public EnumItemDescription(string cultureName, string description)
            : this(CultureInfo.GetCultureInfo(cultureName), description)
        {
        }

        public EnumItemDescription(int cultureId, string description)
            : this(CultureInfo.GetCultureInfo(cultureId), description)
        {
        }

        private EnumItemDescription(CultureInfo culture, string description)
        {
            Culture = culture;
            Description = description;
        }

        public CultureInfo Culture
        {
            get;
            private set;
        }

        public string Description
        {
            get;
            private set;
        }

        public static string GetDisplayValue(int value, Type enumType, CultureInfo culture)
        {
            if (enumType == null || !enumType.IsEnum)
            {
                return null;
            }
            if (!Enum.IsDefined(enumType, value))
            {
                return null;
            }

            if (culture == null)
                return value.ToString();

            Dictionary<CultureInfo, EnumItemDescription> disc = GetDisplayValuesImp(value, enumType);
            if (disc.ContainsKey(culture))
                return disc[culture].Description;
            return value.ToString();
        }

        private static Dictionary<CultureInfo, EnumItemDescription> GetDisplayValuesImp(int value, Type enumType)
        {
            if (!_cache.ContainsKey(enumType))
            {
                Dictionary<int, Dictionary<CultureInfo, EnumItemDescription>> displayValues =
                    new Dictionary<int, Dictionary<CultureInfo, EnumItemDescription>>();
                foreach (var item in Enum.GetValues(enumType))
                {
                    Dictionary<CultureInfo, EnumItemDescription> descriptions =
                        new Dictionary<CultureInfo, EnumItemDescription>();
                    FieldInfo enumField = enumType.GetField(item.ToString());
                    if (enumField != null)
                    {
                        foreach (EnumItemDescription desc
                            in enumField.GetCustomAttributes(typeof(EnumItemDescription), true))
                        {
                            descriptions.Add(desc.Culture, desc);
                        }
                    }
                    displayValues.Add((int)item, descriptions);
                }
                _cache.Add(enumType, displayValues);
            }

            return _cache[enumType][value];
        }
    }
}
