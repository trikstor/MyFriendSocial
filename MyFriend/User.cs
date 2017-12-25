using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MyFriend
{
    public class User
    {
        /*
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDistributedMemoryCache();
            services.AddSession();
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseSession();
            app.Run(async (context) =>
            {
                //TODO:
            });
        }
        */

        public bool TryAuth(string username, string actualPassword)
        {
            var request = $"SELECT password FROM UsersData WHERE username = '{username}'";
            var expectedPassword = new Database().Connect("AuthDatabase").Select(request).First().ToString();

            if (actualPassword != expectedPassword)
                return false;
            
            return true;
        }
    }
}