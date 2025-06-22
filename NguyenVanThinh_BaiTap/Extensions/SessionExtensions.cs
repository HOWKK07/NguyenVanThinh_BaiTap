using Microsoft.AspNetCore.Http;
using Newtonsoft.Json; // Ensure Newtonsoft.Json is installed via NuGet

public static class SessionExtensions
{
    public static void SetObjectAsJson(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value)); // JsonConvert is part of Newtonsoft.Json
    }

    public static T GetObjectFromJson<T>(this ISession session, string key)
    {
        var value = session.GetString(key);
        return value == null ? default : JsonConvert.DeserializeObject<T>(value); // JsonConvert is part of Newtonsoft.Json
    }
}