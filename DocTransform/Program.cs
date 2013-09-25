using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace DocTransform
{
  class Program
  {
    static void Main(string[] args)
    {
      var myXPathDoc = new XPathDocument(args[0]);
      var myXslTrans = new XslCompiledTransform();
      myXslTrans.Load(args[1]);
      var myWriter = new XmlTextWriter(args[2], null);
      myXslTrans.Transform(myXPathDoc, null, myWriter);

      myWriter.Close();
    }
  }
}
