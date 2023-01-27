using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace LuxoftTest.Core
{
   public abstract class BasePage
    {
        private readonly TimeSpan MAXWAITTIME = TimeSpan.FromSeconds(10);

        private readonly WebDriverWait _wait;

        protected readonly IWebDriver _driver;
        protected abstract string Url { get; }

        public BasePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, MAXWAITTIME);
        }

        public virtual void GoTo()
          => _driver.Navigate().GoToUrl(Url);

        public virtual void MaxPage()
            => _driver.Manage().Window.Maximize();

        public virtual void Back()
            => _driver.Navigate().Back();

        public virtual void PageSize(int width, int height)
            => _driver.Manage().Window.Size = new System.Drawing.Size(width, height);

        public IWebElement FindElementByCssSelector(string selector)
        {
            return _wait.Until(dvr => dvr.FindElement(By.CssSelector(selector)));
        }

        public IWebElement FindElementByClassName(string selector)
        {
            return _wait.Until(dvr => dvr.FindElement(By.ClassName(selector)));
        }

        public IList<IWebElement> FindElementsByClassName(string selector)
        {
            return _wait.Until(dvr => dvr.FindElements(By.ClassName(selector)));
        }

        public IWebElement FindElementById(string id)
        {
            return _wait.Until(dvr => dvr.FindElement(By.Id(id)));
        }

        public IWebElement FindElementByName(string name)
        {
            return _wait.Until(dvr => dvr.FindElement(By.Name(name)));
        }

        public IWebElement FindElementByLinkText(string link)
        {
            return _wait.Until(dvr => dvr.FindElement(By.LinkText(link)));
        }


        public IWebElement FindElementByXpath(string xpath)
        {
            return _wait.Until(dvr => dvr.FindElement(By.XPath(xpath)));
        }


        public void AddImplicitWait(IWebDriver driver, int seconds)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(seconds);
        }


        public void HoverElementAndClick(IWebDriver driver, IWebElement elementToHover, IWebElement elementToClick)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(elementToHover).Click(elementToClick).Build().Perform();
        }

        public ReadOnlyCollection<IWebElement> FindElementsByClassName(IWebDriver driver, string selector)
        {
            var lista = driver.FindElements(By.ClassName(selector));
            return lista;
        }

        public IList<IWebElement> FindElementsByXPath(IWebDriver driver, string selector)
        {
            IList<IWebElement>lista = driver.FindElements(By.XPath(selector));
            return lista;
        }
     
        public void SelectDay(IWebDriver driver, IList<IWebElement> listaDias, string dia)
        {
            foreach (var elemento in listaDias)
            {
                if (dia.Equals(elemento.Text)) {
                    elemento.Click();
                    break;
                }                   
            }
        }

        public void ClickElementSendText(IWebDriver driver, IWebElement elementToClick, string texto)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(elementToClick).Click().SendKeys(texto).Build().Perform();
        }

        public void WaitAnElement(IWebDriver driver, int seconds, IWebElement element)
        {
            new WebDriverWait(driver, TimeSpan.FromSeconds(seconds)).Until(ExpectedConditions.ElementToBeClickable(element));
        }

   

    }
}
