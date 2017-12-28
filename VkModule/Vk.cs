using System.Collections.Generic;
using System.Linq;
using VkNet;
using VkNet.Enums.Filters;

namespace VkModule
{
    public class Vk
    {
        private VkApi VkSession { get; }
        private long UserID { get; set; }

        public Vk(string eMail, string password, int applicationID)
        {
            var appID = applicationID;
            var email = eMail;
            var pass = password;
            var scope = Settings.All;

            VkSession = new VkApi();
            VkSession.Authorize(new ApiAuthParams
            {
                ApplicationId = (ulong) appID,
                Login = email,
                Password = pass,
                Settings = scope
            });
        }

        public Vk SetUser(long id)
        {
            VkSession.Users.Get(id);
            UserID = id;

            return this;
        }

        public IEnumerable<string> GetSubscriptions()
        {
            var subscriptions = VkSession.Users.GetSubscriptions(UserID, 200);
            return subscriptions.Select(group => group.Name).ToArray();
        }

        public IEnumerable<string> GetGroups()
        {
            var groups = VkSession.Groups.Get(UserID);
            return groups.Select(group => group.Name).ToArray();
        }
    }
}