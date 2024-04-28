using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Planner_Domain.Helpers.HttpHelpers
{
    public class CustomRequestMessage<T>
    {
        public T? Data { get; set; }
        public Expression? Expression { get; set; }
    }
}