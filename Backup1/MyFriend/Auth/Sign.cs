using System;
using System.Web.UI;

namespace MyFriend.Auth
{
    public class Sign
    {
        private IFormValidator Validator { get; }
        private IUserProvider UserProvider { get; }
        
        public Sign(IFormValidator formValidator, IUserProvider userProvider)
        {
            Validator = formValidator;
            UserProvider = userProvider;
        }
        
        public void In(User actualUser)
        {
            //actualUser = actualUser.ToLower();
            if(!Validator.FieldIsValid(actualUser.Name) 
               && !Validator.FieldIsValid(actualUser.Password))
                throw new ArgumentException("Некорректные данные.");
            
            var expectedUser = UserProvider.GetUser(actualUser.Name);
            if(expectedUser.Password != actualUser.Password)
                throw new AccessViolationException("Неверный пароль.");
        }

        public void Up(User user)
        {
            //user = user.ToLower();
            if(!Validator.FieldIsValid(user.Name) 
               && !Validator.FieldIsValid(user.Password) 
               && !Validator.EmailIsValid(user.Email))
                throw new ArgumentException("Некорректные данные.");
            
            UserProvider.SetUser(user);
        }
    }
}