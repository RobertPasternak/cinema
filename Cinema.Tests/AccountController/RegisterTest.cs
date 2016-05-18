﻿using System;
using Cinema.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Cinema.DAL;

namespace Cinema.Tests.AccountController
{
    [TestClass]
    public class RegisterTest
    {
        private IWebDriver driver;
        static Random rand = new Random();
        private int number;


        [TestInitialize]
        public void SetUp()
        {
            driver = new FirefoxDriver();
            number = rand.Next(1000000000);
            driver.Navigate().GoToUrl("http://localhost:49610/Account/Logout");
        }

        [TestCleanup]
        public void CleanUp()
        {
            driver.Navigate().GoToUrl("http://localhost:49610/Account/Logout");
            driver?.Quit();
        }

        [TestMethod]
        public void UserCreatedSuccessfullyTest()
        {
            string expected = "Przegląd Zamówień - Cinema";
            string actual = null;
            
            driver.Navigate().GoToUrl("http://localhost:49610/Account/Register");
            IWebElement loginTextBox = driver.FindElement(By.Id("Login"));
            IWebElement passwordTextBox = driver.FindElement(By.Id("Password"));
            IWebElement confirmPasswordTextBox = driver.FindElement(By.Id("ConfirmPassword"));
            IWebElement emailTextBox = driver.FindElement(By.Id("Email"));
            IWebElement nameTextBox = driver.FindElement(By.Id("Name"));


            loginTextBox.SendKeys(number + "newusertest");
            passwordTextBox.SendKeys(number + "newusertest");
            confirmPasswordTextBox.SendKeys(number + "newusertest");
            emailTextBox.SendKeys(number + "newusertest@newusertest.pl");
            nameTextBox.SendKeys(number + "newusertest");

            driver.FindElement(By.ClassName("login-button")).Click();

            loginTextBox = driver.FindElement(By.Id("Login"));
            passwordTextBox = driver.FindElement(By.Id("Password"));

            loginTextBox.SendKeys(number + "newusertest");
            passwordTextBox.SendKeys(number + "newusertest");
            driver.FindElement(By.ClassName("login-button")).Click();
            driver.Navigate().GoToUrl("http://localhost:49610/Application/OrderSummary");

            actual = driver.Title;

            Assert.AreEqual(expected, actual);         
        }
    }
}
