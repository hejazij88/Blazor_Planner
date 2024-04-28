using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace Planner_Domain.Helpers.HttpHelpers
{
    public class CustomReferenceResolver : ReferenceResolver
    {

        private uint _referenceCount;

        private readonly Dictionary<string, object> _referenceIdToObjectMap = new();

        private readonly Dictionary<object, string> _objectToReferenceIdMap = new(ReferenceEqualityComparer.Instance);

        public override void AddReference(string referenceId, object value)
        {
            if (!_referenceIdToObjectMap.TryAdd(referenceId, value))
                throw new JsonException();
        }

        public override string GetReference(object value, out bool alreadyExists)
        {
            alreadyExists = false;
            string? referenceId = null;
            object? exist = null;
            dynamic reference = value;

            Guid refId = Guid.Empty;
            try
            {
                refId = reference.Id;
            }
            catch (Exception exception)
            {

            }

            if (refId != Guid.Empty)
            {
                foreach (var key in _objectToReferenceIdMap.Keys)
                {
                    dynamic entity = key;
                    Guid entityId;

                    try
                    {
                        entityId = entity.Id;
                    }
                    catch (Exception exception)
                    {
                        continue;
                    }

                    if (refId != entityId) continue;

                    exist = key;
                    break;
                }
            }

            if (exist != null)
            {
                alreadyExists = true;
                referenceId = _objectToReferenceIdMap[exist];
            }
            else
            {
                if (_objectToReferenceIdMap.TryGetValue(value, out referenceId))
                {
                    alreadyExists = true;
                }
                else
                {
                    _referenceCount++;
                    referenceId = _referenceCount.ToString();
                    _objectToReferenceIdMap.Add(value, referenceId);
                    alreadyExists = false;
                }
            }

            return referenceId;
        }


        public override object ResolveReference(string referenceId)
        {
            if (!_referenceIdToObjectMap.TryGetValue(referenceId, out object? value))
            {
                throw new JsonException();
            }

            return value;
        }
    }
}
