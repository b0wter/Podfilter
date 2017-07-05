using System;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace Podfilter.Helpers
{
    public static class ExtensionMethods
    {
        public static string ToStringWithDeclaration(this XDocument document)
        {
            if(document == null)
                throw new NullReferenceException("The document must not be null.");

            var builder = new StringBuilder();
            using(var writer = new StringWriter(builder))
            {
                document.Save(writer);
            }

            return builder.ToString();
        }
    }
}