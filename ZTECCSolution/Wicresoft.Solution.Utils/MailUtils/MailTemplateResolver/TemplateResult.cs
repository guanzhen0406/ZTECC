using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Wicresoft.Solution.Utils.MailUtils
{
    public class TemplateResult<T> : ActionResult, IDisposable where T : class
    {
        public T Object { get; set; }

        private StringWriter output;

        private string path;

        public TemplateResult(string tPath)
        {
            if (string.IsNullOrEmpty(tPath)) throw new ArgumentNullException("tPath");
            path = tPath;
        }

        public override string Execute()
        {
            if (Object == null) throw new NullReferenceException("Object");
            ObjectToXml<T> xmlTransform = new ObjectToXml<T>(Object);
            XmlDocument doc = xmlTransform.Transform();
            XPathNavigator navgator = doc.CreateNavigator();
            XslCompiledTransform transform = new XslCompiledTransform();
            output = new StringWriter();
            transform.Load(path);
            transform.Transform(navgator, null, output);
            //return output.ToString().Replace("utf-16", "utf-8");
            return output.ToString();
        }

        public void Dispose()
        {
            output.Close();
        }
    }
}
