using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using static PodfilterCore.Models.PodcastModification.Others.RemoveDuplicateEpisodesModification;

namespace PodfilterWeb.Helpers
{
    public static class ExtensionMethods
    {
        public static string ToStringWithDeclaration(this XDocument document)
        {
            if(document == null)
                throw new NullReferenceException("The document must not be null.");

            document.Declaration = new XDeclaration("1.0", "utf-8", null);
            StringWriter writer = new Utf8StringWriter();
            document.Save(writer, SaveOptions.None);

            return writer.ToString();
        }
        private class Utf8StringWriter : StringWriter
        {
            public override Encoding Encoding => Encoding.UTF8;
        }
    }
}