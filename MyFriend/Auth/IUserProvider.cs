using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MyFriend.Auth
{
    public interface IUserProvider
    {
        User GetUser(string name);
        void SetUser(User user);
    }
}