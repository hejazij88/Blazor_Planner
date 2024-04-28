using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Planner_Domain.Helpers.HttpHelpers
{
    public class CustomReferenceHandler : ReferenceHandler
    {
        private CustomReferenceResolver? _rootedResolver;

        public CustomReferenceHandler() => Reset();

        public override ReferenceResolver CreateResolver() => _rootedResolver!;

        public void Reset() => _rootedResolver = new CustomReferenceResolver();
    }
}