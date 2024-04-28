using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Planner_Domain.Helpers.HttpHelpers
{
    [Serializable]
    public class CustomException : Exception
    {
        public HttpStatusCode? DefaultHttpStatusCode { get; set; }

        public CustomException()
        {
        }

        public CustomException(string message)
            : base(message)
        {
        }

        public CustomException(string message, Exception? inner)
            : base(message, inner)
        {
        }

        public CustomException(string message, HttpStatusCode defaultHttpStatusCode)
            : this(message)
        {
            DefaultHttpStatusCode = defaultHttpStatusCode;
        }

        public CustomException(string message, Exception? inner, HttpStatusCode defaultHttpStatusCode)
            : base(message, inner)
        {
            DefaultHttpStatusCode = defaultHttpStatusCode;
        }
    }
}