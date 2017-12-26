using System.Linq;
using MyFriend.Controllers;

namespace MyFriend
{
    public class Users
    {
        public bool TryAuth(string username, string actualPassword)
        {
            var request = $"SELECT password FROM UsersData WHERE username = '{username.ToLower()}'";
            if (!new Database().Connect("AuthDatabase").CheckEquality(request, actualPassword)) 
                return false;
            return true;
        }
        
        public bool TryRegister(string username, string password, string email)
        {
            var db = new Database();   
            var requests = new[]
            {
                $"SELECT * FROM UsersData WHERE username = '{username.ToLower()}'",
                $"SELECT * FROM UsersData WHERE email = '{email.ToLower()}'"
            };
            return db.Connect("AuthDatabase").Select(requests).ToArray().Length != 0;
        }
    }
}