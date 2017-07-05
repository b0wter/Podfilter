using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podfilter.Data
{
    public class EAV_Entity
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
    }

    public class EAV_Envtity<T> : EAV_Entity
    {
        public T Value { get; set; }
    }
}
