using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Podfilter.Models
{
    public interface IFilter
    {
        // IFilter is a non-generic interface because instances with different types need to be stored in a single List<IFilter>.
        
        bool PassesFilter(object obj);
    }
}