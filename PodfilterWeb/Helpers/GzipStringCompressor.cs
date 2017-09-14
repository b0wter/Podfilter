using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace PodfilterWeb.Helpers
{
    public class GzipStringCompressor : BaseStringCompressor
    {
        public override byte[] CompressUnicode(string content)
        {
            var bytes = Encoding.Unicode.GetBytes(content);
            using (var msi = new MemoryStream(bytes))
            using (var mso = new MemoryStream()) {
                using (var gs = new GZipStream(mso, CompressionMode.Compress)) 
                {
                    msi.CopyTo(gs);
                }
                return mso.ToArray();
            }
        }

        public override string DecompressToUnicode(byte[] content)
        {
            using (var msi = new MemoryStream(content))
            using (var mso = new MemoryStream()) 
            {
                using (var gs = new GZipStream(msi, CompressionMode.Decompress)) 
                {
                    gs.CopyTo(mso);
                }
                return Encoding.Unicode.GetString(mso.ToArray());
            }            
        }
    }
}