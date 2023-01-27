using LuxoftTest.POM;
using LuxoftTest.TestFlow;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Configuration;
using System.Threading;

namespace LuxoftTest
{
    public class Tests
    {
        private IWebDriver driver;       
        private readonly string _configValue = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath;
        bool result = false;

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--disable-blink-features=AutomationControlled");
            driver = new ChromeDriver(options);           
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual("https://aeromexico.com/", ConfigurationSettings.AppSettings["URL"]);
            Assert.AreEqual("QA1", ConfigurationSettings.AppSettings["Enviroment"]);
            var  objFligh = new FLBuyFlight(driver);
            objFligh.InitializePage();
            objFligh.changeCurrency();
            objFligh.fillDataTrip();
            result = objFligh.Result;
            Assert.IsTrue(result);
            Assert.AreEqual(driver.Title, "Aeromexico");
            Assert.Pass();
        }

        [TearDown]
        public void Close()
        {
            driver.Close();
            Console.WriteLine("Test executed");
        }
    }
}