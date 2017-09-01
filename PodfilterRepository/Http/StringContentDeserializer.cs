using PodfilterCore.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace PodfilterRepository.Http
{
    public class StringContentDeserializer : IContentDeserializer<string>
    {
        public string DeserializeString(string s)
        {
            if (s == null)
                throw new ArgumentNullException();

            return s;
        }
    }
}
