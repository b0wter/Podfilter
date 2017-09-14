using System;
using Microsoft.AspNetCore.WebUtilities;

namespace PodfilterWeb.Helpers
{
    public abstract class BaseStringCompressor
    {
        public abstract byte[] CompressUnicode(string content);

        public string CompressAndBase64UrlEncodeUnicode(string content)
        {
            var bytes = CompressUnicode(content);
            var base64 = Base64UrlTextEncoder.Encode(bytes);
            return base64;
        }

        public abstract string DecompressToUnicode(byte[] content);

        public string Base64UrlDecodeAndDecompressToUnicode(string content)
        {
            var bytes = Base64UrlTextEncoder.Decode(content);
            var decoded = DecompressToUnicode(bytes);
            return decoded;
        }
    }
}