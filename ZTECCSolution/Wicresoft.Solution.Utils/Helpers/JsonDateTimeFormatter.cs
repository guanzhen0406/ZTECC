using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wicresoft.Solution.Utils
{
    public class JsonDateTimeFormatter : DateTimeConverterBase
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            DateTime date = new DateTime();
            DateTime.TryParse((string)reader.Value, out date);
            if (date == default(DateTime))
            {
                return null;
            }
            return date;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var time = (DateTime)value;
            if (time == default(DateTime))
            {
                writer.WriteValue("");
            }
            else
            {
                writer.WriteValue(time.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
    }
}
