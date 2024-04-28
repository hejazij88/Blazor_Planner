using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Planner_Domain.Helpers.HttpHelpers
{
    public class CustomResponseMessage<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? MessageForUser { get; set; }
    }
}
