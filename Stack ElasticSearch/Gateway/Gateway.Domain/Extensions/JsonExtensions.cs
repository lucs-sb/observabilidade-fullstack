using System.Text.Json;

namespace Gateway.Domain.Extensions;

public static class JsonExtensions
{
    public static string SerializeObject(this object response)
    {
        return JsonSerializer.Serialize(response);
    }

    public static T DeserializeObject<T>(this string response)
    {
        return JsonSerializer.Deserialize<T>(response)!;
    }
}
