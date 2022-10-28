using FluentAssertions;
using Newtonsoft.Json;

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
}