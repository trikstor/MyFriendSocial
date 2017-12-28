using FluentAssertions;
using MyFriend.Auth;
using NUnit.Framework;

namespace MyFriend.Tests
{
    [TestFixture]
    public class FormValidatorShould
    {
        private FormValidator FormValidator;
        
        [SetUp]
        public void SetUp()
        {
            FormValidator = new FormValidator();
        }

        [TestCase("username", ExpectedResult = true, TestName = "Correct field text.")]
        [TestCase("us", ExpectedResult = false, TestName = "Too short text.")]
        [TestCase("usernameusernameusername", ExpectedResult = false, TestName = "Too long text.")]
        [TestCase("u\"s\\e\tr", ExpectedResult = false, TestName = "Special characters in the text.")]
        public bool FieldIsValid_ReturnCorrectCondition(string input)
        {
            return FormValidator.FieldIsValid(input);
        }
        
        [TestCase("user@gmail.com", ExpectedResult = true, TestName = "Correct email.")]
        [TestCase("user@gmailcom", ExpectedResult = false, TestName = "Incorrect email.")]
        [TestCase("gmail.com", ExpectedResult = false, TestName = "Only domain.")]
        [TestCase("usergmailcom", ExpectedResult = false, TestName = "Just a string.")]
        public bool EmailIsValid_ReturnCorrectCondition(string input)
        {
            return FormValidator.EmailIsValid(input);
        }
    }
}