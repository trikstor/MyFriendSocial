using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;

namespace MyFriend.Auth
{
    public class UserProvider : IUserProvider
    {
        private string FileDataPath { get; }
        private DataContractJsonSerializer JsonFormatter { get; }
        
        public UserProvider(string fileDataPath)
        {
            JsonFormatter = new DataContractJsonSerializer(typeof(User));
            FileDataPath = fileDataPath;
        }
        
        public User GetUser(string name)
        {
            User currUser;
            using (var fs = new FileStream(FileDataPath, FileMode.OpenOrCreate))
            {
                currUser = (User) JsonFormatter.ReadObject(fs);
                while (currUser.Name != name)
                {
                    currUser = (User) JsonFormatter.ReadObject(fs);
                    if(currUser == null)
                        throw new KeyNotFoundException("Such a user is not found.");
                }
            }
            return currUser;
        }

        public void SetUser(User user)
        {
            using (var fs = new FileStream(FileDataPath, FileMode.OpenOrCreate))
            {
                JsonFormatter.WriteObject(fs, user);
            }
        }
    }
}