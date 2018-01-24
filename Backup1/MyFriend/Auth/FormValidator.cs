using System;
using EmailValidation;

namespace MyFriend.Auth
{
    public class FormValidator : IFormValidator
    {       
        public bool FieldIsValid(string field)
        {
            var specialCharacters = new[]
            {
                '\"',
                '\'',
                '\\',
                '{',
                '}'
            };
            return field.Length > 2 && field.Length < 15 && field.NotContains(specialCharacters);
        }

        public bool EmailIsValid(string email)
        {
            var tt = EmailValidator.Validate(email);
            Console.WriteLine(tt);
            return tt;
        }
    }
}