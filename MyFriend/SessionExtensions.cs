using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MyFriend
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.Set(key, JsonConvert.SerializeObject(value));
        }
 
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.Get<T>(key);

            return value == null
                ? default(T)
                : value;
        }
    }
}