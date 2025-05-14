using Mashkarin777;
using NUnit.Framework;
using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace Mashkarin777.Tests
{
    [TestFixture]
    public class SignInTests
    {
        private RegPage regPage;

        [SetUp]
        public void Setup()
        {
            regPage = new RegPage();
        }

        [Test]
        public void TestPasswordTooShort()
        {
            regPage.Extra2txtBox.Text = "Short1!";
            regPage.BtnSingIn_Click(null, null);

            var errorMessage = GetErrorMessage();
            Assert.IsTrue(errorMessage.Contains("Пароль должен содержать хотя бы 8 символов"));
        }

        [Test]
        public void TestPasswordNoLowerCaseLetter()
        {
            regPage.Extra2txtBox.Text = "PASSWORD123!";
            regPage.BtnSingIn_Click(null, null);

            var errorMessage = GetErrorMessage();
            Assert.IsTrue(errorMessage.Contains("Пароль должен содержать хотя бы одну строчную букву"));
        }

        [Test]
        public void TestPasswordNoUpperCaseLetter()
        {
            regPage.Extra2txtBox.Text = "password123!";
            regPage.BtnSingIn_Click(null, null);

            var errorMessage = GetErrorMessage();
            Assert.IsTrue(errorMessage.Contains("Пароль должен содержать хотя бы одну прописную букву"));
        }

        [Test]
        public void TestPasswordNoDigit()
        {
            regPage.Extra2txtBox.Text = "Password!";
            regPage.BtnSingIn_Click(null, null);

            var errorMessage = GetErrorMessage();
            Assert.IsTrue(errorMessage.Contains("Пароль должен содержать хотя бы одну цифру"));
        }

        [Test]
        public void TestPasswordNoSpecialCharacter()
        {
            regPage.Extra2txtBox.Text = "Password123";
            regPage.BtnSingIn_Click(null, null);

            var errorMessage = GetErrorMessage();
            Assert.IsTrue(errorMessage.Contains("Пароль должен содержать хотя бы один специальный символ"));
        }

        [Test]
        public void TestPasswordValid()
        {
            regPage.Extra2txtBox.Text = "Password123!";
            regPage.BtnSingIn_Click(null, null);

            var errorMessage = GetErrorMessage();
            Assert.IsEmpty(errorMessage);
        }

        private string GetErrorMessage()
        {
            return regPage.errStr.ToString();
        }
    }
}