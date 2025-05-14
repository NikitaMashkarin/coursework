using NUnit.Framework;
using System;
using System.Text;

namespace Mashkarin777.Tests
{
    [TestFixture]
    public class ValidateTests
    {
        private Validate validate;

        [SetUp]
        public void SetUp()
        {
            validate = new Validate();
        }

        [Test]
        [STAThread]
        public void TestPasswordTooShort()
        {
            string password = "Short1!";
            string result = validate.ValidatePassword(password);
            Assert.IsTrue(result.Contains("Пароль должен содержать хотя бы 8 символов"));
        }

        [Test]
        [STAThread]
        public void TestPasswordNoLowerCaseLetter()
        {
            string password = "PASSWORD123!";
            string result = validate.ValidatePassword(password);
            Assert.IsTrue(result.Contains("Пароль должен содержать хотя бы одну строчную букву"));
        }

        [Test]
        [STAThread]
        public void TestPasswordNoUpperCaseLetter()
        {
            string password = "password123!";
            string result = validate.ValidatePassword(password);
            Assert.IsTrue(result.Contains("Пароль должен содержать хотя бы одну прописную букву"));
        }

        [Test]
        [STAThread]
        public void TestPasswordNoDigit()
        {
            string password = "Password!";
            string result = validate.ValidatePassword(password);
            Assert.IsTrue(result.Contains("Пароль должен содержать хотя бы одну цифру"));
        }

        [Test]
        [STAThread]
        public void TestPasswordNoSpecialCharacter()
        {
            string password = "Password123";
            string result = validate.ValidatePassword(password);
            Assert.IsTrue(result.Contains("Пароль должен содержать хотя бы один специальный символ"));
        }

        [Test]
        [STAThread]
        public void TestPasswordValid()
        {
            string password = "Password123!";
            string result = validate.ValidatePassword(password);
            Assert.IsEmpty(result);
        }
    }
}
