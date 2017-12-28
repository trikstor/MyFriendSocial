using System;
using System.Web.UI;

namespace MyFriend.Auth
{
    public class Sign
    {
        private IFormValidator FormValidator { get; }
        private IUserProvider UserProvider { get; }
        
        public Sign(IFormValidator formValidator, IUserProvider userProvider)
        {
            FormValidator = formValidator;
            UserProvider = userProvider;
        }
        
        public void In(User actualUser)
        {
            //actualUser = actualUser.ToLower();
            if(FormValidator.FieldIsValid(actualUser.Name) 
               && FormValidator.FieldIsValid(actualUser.Password) 
               && FormValidator.EmailIsValid(actualUser.Email))
                throw new ArgumentException("Некорректные данные.");
            
            var expectedUser = UserProvider.GetUser(actualUser.Name);
            if(expectedUser.Password != actualUser.Password)
                throw new AccessViolationException("Неверный пароль.");
        }

        public void Up(User user)
        {
            //user = user.ToLower();
            if(FormValidator.FieldIsValid(user.Name) 
               && FormValidator.FieldIsValid(user.Password) 
               && FormValidator.EmailIsValid(user.Email))
                throw new ArgumentException("Некорректные данные.");
            
            UserProvider.SetUser(user);
        }
    }
}