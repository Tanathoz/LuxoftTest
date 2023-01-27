using System;
using System.Collections.Generic;
using System.Text;
using LuxoftTest.Core;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Configuration;
using System.Globalization;
using System.Linq;

namespace LuxoftTest.POM
{
    public class POHome: BasePage
    {

        string url2 = ConfigurationManager.AppSettings.Get("URL");
        protected override string Url => url2;
        
        public POHome(IWebDriver driver) : base(driver) { }

    
        public IWebElement LiCurrency => FindElementById("mcp");
        public IWebElement ButtonCurrency => FindElementByClassName("Header-currencyItems");
        public IWebElement ModalWindow => FindElementByClassName("Modal-content--STOREFRONT_SELECTOR_MODAL");

        public IWebElement DivLanguageEnglish => FindElementByXpath("//*[@id='content']/div/div/div/div[2]/div/div[2]/div/div/div[2]/div/div[2]/div/div[2]");

        public IWebElement ModalCookies => FindElementByClassName("Modal-content--HOME_COOKIES");
        public IWebElement ButtonAcceptCookies => FindElementByClassName("CookiesModal--btn");
        public IWebElement InputTextOrigin => FindElementByName("origin");
        public IWebElement InputTextDestination => FindElementByName("destination");

        public IWebElement DivCalendarPicker => FindElementByClassName("BookerCalendarPicker");
        public IWebElement ButtonNextMonth => FindElementByClassName("DatePickerRefactored-arrowRight");
        public IWebElement ButtonFindFlights => FindElementByClassName("VacationBooker-submitBtn");
        public IList<IWebElement> DivPriceFlight => FindElementsByClassName("FlightOptionsFare-price");
        public IList<IWebElement> DivHourFlight => FindElementsByClassName("FlightOptionsTimeline-time");

        public IWebElement SpanChooseFlight => FindElementByXpath("//span[contains(text(),'Choose your departing flight')]");

        public IWebElement DivSortFlights => FindElementByClassName("FlightOptionsFilterSort-button");



        public void RecorrePrecios(IWebDriver driver, IList<IWebElement> listaPrecios, IList<IWebElement> listaHorarios)
        {
            int cont = 0;
            double mayor = double.Parse(listaPrecios[0].Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Currency), menor = double.Parse(listaPrecios[0].Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Currency);
            string textExpensivest=String.Empty, textCheapest= String.Empty;
            foreach (var elemento in listaPrecios)
            {
                var hora = listaHorarios[cont].Text;
                var precio = double.Parse(elemento.Text, NumberStyles.AllowCurrencySymbol | NumberStyles.Currency);
                var precioText = elemento.Text;
                if (precio > mayor)
                {
                    mayor = precio;
                    textExpensivest = $"The Expensivest flight cost :{mayor} -Departure time- {listaHorarios[cont].Text} -Arrival time- {listaHorarios[cont + 1].Text} "  ;
                }
                if (precio < menor)
                {
                    menor = precio;
                    textCheapest = $"The Cheapest flight cost :{menor} -Departure time- {listaHorarios[cont].Text} -Arrival time- {listaHorarios[cont + 1].Text} ";
                }
                cont= cont+2;
            }
            Console.WriteLine(textExpensivest);
            Console.WriteLine(textCheapest);
        }


    }
}
