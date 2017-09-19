using System;
using System.Collections.Generic;
using System.Text;

namespace PodfilterCore.Exceptions.Repository
{
    public class RepositoryException : Exception
    {
        public RepositoryException()
            : base()
        {
            //
        }

        public RepositoryException(string message)
            : base(message)
        {
            //
        }
    }
}
