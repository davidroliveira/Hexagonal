using FluentAssertions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Projeto.Base.Common.Helpers;

public static class ObjectHelper
{
    public static bool Compare(this object self, object other)
    {
        var selfJson = JsonConvert.SerializeObject(self);
        var otherJson = JsonConvert.SerializeObject(other);
        selfJson.Should().BeEquivalentTo(otherJson);
        return true;
    }

    public static string SerializeAsString(this object self, bool includeNullValues = true, bool camelCasePropertyNames = true)
    {
        JsonSerializerSettings serializerSettings = new()
        {
            ContractResolver = camelCasePropertyNames
                ? new CamelCasePropertyNamesContractResolver()
                : new DefaultContractResolver(),

            NullValueHandling = includeNullValues
                ? NullValueHandling.Include
                : NullValueHandling.Ignore
        };

        var result = JsonConvert.SerializeObject(self, serializerSettings);
        return result;
    }
}