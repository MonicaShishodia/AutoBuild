﻿using TestCI;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;

namespace TestCI.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private string baseURL = "http://partsunlimited.azurewebsites.net/";
        private RemoteWebDriver driver;
        private string browser;
        public TestContext TestContext { get; set; }
        IPrintMessage ipm = new GetData();

        [TestMethod()]
        public void GetMessageTest()
        {
            Assert.IsNotNull(ipm.GetMessage());
        }
        [TestMethod()]
        public void GetExecutionTime()
        {
            System.Threading.Thread.Sleep(10000);
        }

        [TestMethod]
        [TestCategory("Selenium")]
        [Priority(5)]
        [Owner("BuildSet")] //Using Owner as Category trait is not supported by the DTA Task
        public void TireSearch_Any()
        {
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(30));
            driver.Navigate().GoToUrl(this.baseURL);
            driver.FindElementByName("btnI").Click();
            //driver.FindElementById("lst-ib").Clear();
            //driver.FindElementById("lst-ib").SendKeys("Selenium");
            //do other Selenium things here!
        }

        /// <summary>
        /// Use TestCleanup to run code after each test has run
        /// </summary>
        [TestCleanup()]
        public void MyTestCleanup()
        {
            driver.Quit();
        }
        [TestInitialize()]
        public void MyTestInitialize()
        {   //Set the browswer from a build

            browser = this.TestContext.Properties["browser"] != null ? this.TestContext.Properties["browser"].ToString() : "chrome";
            switch (browser)
            {
                case "firefox":
                    driver = new FirefoxDriver();
                    break;
                case "chrome":
                    driver = new ChromeDriver();
                    break;
                case "ie":
                    driver = new InternetExplorerDriver();
                    break;
                case "PhantomJS":
                    driver = new PhantomJSDriver();
                    break;
                default:
                    driver = new PhantomJSDriver();
                    break;
            }
            if (this.TestContext.Properties["Url"] != null) //Set URL from a build
            {
                this.baseURL = this.TestContext.Properties["Url"].ToString();
            }
            else
            {
                this.baseURL = "http://google.com/";   //default URL just to get started with
            }
        }
    }
}
