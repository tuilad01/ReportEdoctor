using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace ConsoleApp1
{
    class Program
    {
        private static void LoginGmail(ChromeDriver driver, string username, string password)
        {
            //send username
            driver.FindElement(By.Id("identifierId")).SendKeys(username);

            //press next button
            driver.FindElement(By.Id("identifierNext")).Click();

            //wait password visible
            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
                .Until(ExpectedConditions.ElementIsVisible(By.XPath("//input[@type='password']")));
            driver.FindElement(By.XPath("//input[@type='password']")).SendKeys(password);

            new WebDriverWait(driver, TimeSpan.FromSeconds(10))
            .Until(ExpectedConditions.ElementToBeClickable(By.Id("passwordNext")));
            //press next login
            driver.FindElement(By.Id("passwordNext")).Click();
        }

        static void Main(string[] args)
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            using (var driver = new ChromeDriver(options))
            {
                driver.Navigate().GoToUrl("https://staff.edoctor.io/");

                var loginButton = driver.FindElementById("loginByeDoctorEmail");

                loginButton.Click();                
                driver.SwitchTo().Window(driver.WindowHandles[1]);

                LoginGmail(driver, "minhnn@edoctor.vn", "minh130107");                

                driver.SwitchTo().Window(driver.WindowHandles[0]);                             

                (new WebDriverWait(driver, TimeSpan.FromSeconds(10)))
                    .Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("Vận hành")));

                driver.FindElement(By.PartialLinkText("Vận hành")).Click();

                (new WebDriverWait(driver, TimeSpan.FromSeconds(10)))
                    .Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("Đơn tôi triển khai")));

                driver.FindElement(By.PartialLinkText("Đơn tôi triển khai")).Click();

                (new WebDriverWait(driver, TimeSpan.FromSeconds(10)))
                    .Until(ExpectedConditions.ElementToBeClickable(By.XPath("//button[text()='Tất cả thời gian']")));

                driver.FindElements(By.XPath("//button[text()='Tất cả thời gian']"))[1].Click();

                driver.FindElements(By.XPath("//li[@data-range-key='Ngày mai' or text()='Ngày mai']"))[1].Click();

                var patientInfo = driver.FindElements(By.XPath("//table//div[@class='patient-info']"));

                foreach(var pInfo in patientInfo)
                {
                    var name = pInfo.FindElement(By.ClassName("text-strong mb-1")).Text;

                    Console.WriteLine("name: " + name);

                    var tagpInfo = pInfo.FindElements(By.XPath("p"));

                    var phoneNumber = tagpInfo[0].FindElement(By.TagName("a")).Text;

                    if (tagpInfo.Count < 6)
                    {

                    } else
                    {

                    }

                }


                Console.ReadKey();
                
                //driver.GetScreenshot().SaveAsFile(@"screen.png", OpenQA.Selenium.ScreenshotImageFormat.Png);
            }
        }
    }
}
