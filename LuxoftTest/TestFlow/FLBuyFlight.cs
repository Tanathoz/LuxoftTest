using LuxoftTest.POM;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuxoftTest.TestFlow
{
    public class FLBuyFlight
    {
        protected readonly IWebDriver driver;
        private POHome homePage = null;
        private bool result;

        public bool Result
        {
            get
            {
                return result;
            }
            set
            {
                if (value == null)
                    this.result = false;
                else
                    this.result = value;
            }

        }
        public FLBuyFlight(IWebDriver _driver)
        {
            driver = _driver;
            homePage = new  POHome(driver);
        }

        public void InitializePage()
        {
            homePage.GoTo();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            homePage.MaxPage();
        }

        public void changeCurrency()
        {                 
            homePage.ButtonCurrency.Click();
            homePage.DivLanguageEnglish.Click();
            homePage.AddImplicitWait(driver, 5);
            if (homePage.ModalCookies.Enabled)
            {
                homePage.ButtonAcceptCookies.Click();
            }

            if (!homePage.ButtonCurrency.Text.Equals("English - USD"))
            {
                Assert.Fail();
            }
        }

        public void fillDataTrip()
        {
            homePage.ClickElementSendText(driver, homePage.InputTextOrigin, "Guadalajara");
            homePage.AddImplicitWait(driver, 3);
            homePage.InputTextOrigin.SendKeys(Keys.Enter);
            homePage.ClickElementSendText(driver, homePage.InputTextDestination, "Seattle");
            homePage.AddImplicitWait(driver, 2);
            homePage.InputTextDestination.SendKeys(Keys.Enter);
            homePage.DivCalendarPicker.Click();
            homePage.ButtonNextMonth.Click();
            var lista = homePage.FindElementsByClassName(driver, "DatePickerCalendarMonthRefactored-dayNumber");
            homePage.SelectDay(driver, lista, "3");
            homePage.SelectDay(driver, lista, "28");
            homePage.AddImplicitWait(driver, 1);
            homePage.ButtonFindFlights.Click();
            homePage.AddImplicitWait(driver, 15);
            homePage.RecorrePrecios(driver, homePage.DivPriceFlight, homePage.DivHourFlight);
            if (homePage.SpanChooseFlight.Displayed)
                result = true;
            else
                result = false;
            

        }

        
    }
}
