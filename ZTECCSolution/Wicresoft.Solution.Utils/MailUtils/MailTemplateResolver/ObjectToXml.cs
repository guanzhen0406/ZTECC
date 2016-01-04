using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Wicresoft.Solution.Utils.MailUtils
{
    public class ObjectToXml<T> where T : class
    {
        private T Object { get; set; }

        private XmlTextWriter writer;

        public ObjectToXml(T _object)
        {
            Object = _object;
        }


        public XmlDocument Transform()
        {
            string xml = XmlSerializer<T>(Object);
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(xml);
            return xd;
        }


        public string XmlSerializer<X>(X serialObject) where X : class
        {
            XmlSerializer ser = new XmlSerializer(typeof(X));
            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            writer = new XmlTextWriter(mem, Encoding.UTF8);
            ser.Serialize(writer, serialObject);
            writer.Close();
#if DEBUG
            Debug.WriteLine(Encoding.UTF8.GetString(mem.ToArray()));
#endif
            byte[] array = mem.ToArray().Skip(3).ToArray();
            return Encoding.UTF8.GetString(array);
        }

        public string XmlSerializer()
        {
            XmlSerializer ser = new XmlSerializer(typeof(T));
            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            writer = new XmlTextWriter(mem, Encoding.UTF8);
            ser.Serialize(writer, Object);
            writer.Close();
#if DEBUG
            Debug.WriteLine(Encoding.UTF8.GetString(mem.ToArray()));
#endif
            byte[] array = mem.ToArray().Skip(3).ToArray();
            return Encoding.UTF8.GetString(array);
        }
    }
}
