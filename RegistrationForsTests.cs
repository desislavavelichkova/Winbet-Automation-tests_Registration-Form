using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace WinBet_RegistrationTests
{
    public class RegistrationFormTests
    {
        public const string url = "https://winbet.bg/registration";
        private WebDriver driver;

        [SetUp]
        public void OpenBrowser()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [Test]
        public void Test_CreateNewUser_WithInvalidFirstName_EmptyString_ThrowErrorMessage()
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.CssSelector(".btn.btn-primary.ml-auto")).Click();

            var firstNameBox = driver.FindElement(By.Name("firstName"));
            firstNameBox.SendKeys("");
            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.Click();

            string expectedMessage = "Полето за име е задължително";
            var errorMessage = driver.FindElement(By.CssSelector(".bg--error.d-flex-ac.error.form-message.w-100 > .flex-auto"));
    
            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));
        }

        [TestCase("N")]
        [TestCase("@")]
        [TestCase(" ")]
        public void Test_CreateNewUser_WithInvalidFirstName_StringLessThanTwoSimbols_ThrowErrorMessage(string name)
        {
            driver.Navigate().GoToUrl(url);
            var firstNameBox = driver.FindElement(By.Name("firstName"));
            firstNameBox.SendKeys(name);
            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.Click();

            var expectedMessage = "Името трябва да бъде минимум 2 символа";
            var errorMessage = driver.FindElement(By.CssSelector(".bg--error.d-flex-ac.error.form-message.w-100 > .flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));
        }

        [TestCase("InvalidName1234")]
        [TestCase("Invalid Name")]
        [TestCase("111")]
        [TestCase("!@#$%^&")]
        [TestCase("   ")]      

        public void Test_CreateNewUser_WithInvalidFirstName_ThrowErrorMessage(string name)
        {
            driver.Navigate().GoToUrl(url);
            var firstNameBox = driver.FindElement(By.Name("firstName"));
            firstNameBox.SendKeys(name);
            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.Click();

            var expectedMessage = "Моля, въведете само валидни символи";
            var errorMessage = driver.FindElement(By.CssSelector(".bg--error.d-flex-ac.error.form-message.w-100 > .flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void Test_CreateNewUser_WithInvalidLastName_EmptyString_ThrowErrorMessage()
        {
            driver.Navigate().GoToUrl(url);
            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.SendKeys("");

            driver.FindElement(By.ClassName("auth-page__body")).Click();

            string expectedMessage = "Полето за фамилия е задължително";
            var errorMessage = driver.FindElement(By.CssSelector(".bg--error.d-flex-ac.error.form-message.w-100 > .flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));
        }

        [TestCase("N")]
        [TestCase("@")]
        [TestCase(" ")]
        public void Test_CreateNewUser_WithInvalidLastName_StringLessThanTwoSimbols_ThrowErrorMessage(string name)
        {
            driver.Navigate().GoToUrl(url);
            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.SendKeys(name);

            driver.FindElement(By.ClassName("auth-page__body")).Click();

            var expectedMessage = "Фамилията трябва да бъде минимум 2 символа";
            var errorMessage = driver.FindElement(By.CssSelector(".bg--error.d-flex-ac.error.form-message.w-100 > .flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));
        }

        [TestCase("InvalidName1234")]
        [TestCase("Invalid Name")]
        [TestCase("111")]
        [TestCase("!@#$%^&")]
        [TestCase("   ")]
        public void Test_CreateNewUser_WithInvalidLastName_ThrowErrorMessage(string name)
        {
            driver.Navigate().GoToUrl(url);

            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.SendKeys(name);

            driver.FindElement(By.ClassName("auth-page__body")).Click();

            var expectedMessage = "Моля, въведете само валидни символи";
            var errorMessage = driver.FindElement(By.CssSelector(".bg--error.d-flex-ac.error.form-message.w-100 > .flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));
        }

        [TestCase("0", "1", "2022")]
        [TestCase("-1", "1", "2022")]
        [TestCase("@", "1", "2022")]
        [TestCase("Aa", "1", "2022")]
        [TestCase("a", "1", "2022")]
        public void Test_CreateNewUser_WithInvalidDayOfBirth_ThrowErrorMessage(string day, string month, string year)
        {
            driver.Navigate().GoToUrl(url);

            var dayBox = driver.FindElement(By.Name("day"));
            dayBox.SendKeys(day);

            var monthBox = driver.FindElement(By.Name("month"));
            monthBox.SendKeys(month);

            var yearBox = driver.FindElement(By.Name("year"));
            yearBox.SendKeys(year);

            driver.FindElement(By.ClassName("auth-page__body")).Click();

            var expectedMessage = "Датата не е валидна";
            var errorMessage = driver.FindElement(By.CssSelector(".flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));
        }

        [TestCase("1", "0", "2022")]
        [TestCase("1", "-1", "2022")]
        [TestCase("1", "@", "2022")]
        [TestCase("1", "Aa", "2022")]
        [TestCase("1", "a", "2022")]
        public void Test_CreateNewUser_WithInvalidMonthOfBirth_ThrowErrorMessage(string day, string month, string year)
        {
            driver.Navigate().GoToUrl(url);

            var dayBox = driver.FindElement(By.Name("day"));
            dayBox.SendKeys(day);

            var monthBox = driver.FindElement(By.Name("month"));
            monthBox.SendKeys(month);

            var yearBox = driver.FindElement(By.Name("year"));
            yearBox.SendKeys(year);

            driver.FindElement(By.ClassName("auth-page__body")).Click();

            var expectedMessage = "Датата не е валидна";
            var errorMessage = driver.FindElement(By.CssSelector(".flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));
        }
        [TestCase("1","1", "0")]
        [TestCase("1","1", "-1")]
        [TestCase("1", "1", "@")]
        [TestCase("1", "1", "Aa")]
        [TestCase("1", "1", "a")]
        public void Test_CreateNewUser_WithInvalidYearOfBirth_ThrowErrorMessage(string day, string month, string year)
        {
            driver.Navigate().GoToUrl(url);

            var dayBox = driver.FindElement(By.Name("day"));
            dayBox.SendKeys(day);

            var monthBox = driver.FindElement(By.Name("month"));
            monthBox.SendKeys(month);

            var yearBox = driver.FindElement(By.Name("year"));
            yearBox.SendKeys(year);

            driver.FindElement(By.ClassName("auth-page__body")).Click();

            var expectedMessage = "Датата не е валидна";
            var errorMessage = driver.FindElement(By.CssSelector(".flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));
        }
        [Test]
        public void Test_CreateNewUser_WithValidData()
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.CssSelector(".btn.btn-primary.ml-auto")).Click();
            
            var firstNameBox = driver.FindElement(By.Name("firstName"));
            firstNameBox.SendKeys("FirstName");

            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.SendKeys("LastName");

            var dayBox = driver.FindElement(By.Name("day"));
            dayBox.SendKeys("1");

            var monthBox = driver.FindElement(By.Name("month"));
            monthBox.SendKeys("1");

            var yearBox = driver.FindElement(By.Name("year"));
            yearBox.SendKeys("2000");

            var maleBox = driver.FindElement(By.CssSelector("div:nth-of-type(1) > .fcw.fcw-radio > .d-flex-ac.radio-wrapper"));
            maleBox.Click();

            var submitBtn = driver.FindElement(By.CssSelector("form > .btn.btn-action.undefined"));                        

            Assert.That(submitBtn.GetDomProperty("disabled"), Is.EqualTo("False"));
        }

        [TestCase("")]
        public void Test_CreateNewUser_WithInvalidEmail_EmptyString(string email)
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.CssSelector(".btn.btn-primary.ml-auto")).Click();

            var firstNameBox = driver.FindElement(By.Name("firstName"));
            firstNameBox.SendKeys("FirstName");

            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.SendKeys("LastName");

            var dayBox = driver.FindElement(By.Name("day"));
            dayBox.SendKeys("1");

            var monthBox = driver.FindElement(By.Name("month"));
            monthBox.SendKeys("1");

            var yearBox = driver.FindElement(By.Name("year"));
            yearBox.SendKeys("2000");

            var maleBox = driver.FindElement(By.CssSelector("div:nth-of-type(1) > .fcw.fcw-radio > .d-flex-ac.radio-wrapper"));
            maleBox.Click();

            driver.FindElement(By.CssSelector("form > .btn.btn-action.undefined")).Click();

            var emailBox = driver.FindElement(By.Name("email"));
            emailBox.SendKeys(email);

            driver.FindElement(By.CssSelector(".auth-page__body")).Click();

            string expectedMessage = "Въвеждането на имейл е задължително";
            var errorMessage = driver.FindElement(By.CssSelector(".bg--error.d-flex-ac.error.form-message.w-100 > .flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));

        }

        [TestCase("1")]
        [TestCase("a")]
        [TestCase("aa")]
        [TestCase("@abv")]
        [TestCase("@abv")]        
        public void Test_CreateNewUser_WithInvalidEmail_LessThenFiveChars(string email)
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.CssSelector(".btn.btn-primary.ml-auto")).Click();

            var firstNameBox = driver.FindElement(By.Name("firstName"));
            firstNameBox.SendKeys("FirstName");

            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.SendKeys("LastName");

            var dayBox = driver.FindElement(By.Name("day"));
            dayBox.SendKeys("1");

            var monthBox = driver.FindElement(By.Name("month"));
            monthBox.SendKeys("1");

            var yearBox = driver.FindElement(By.Name("year"));
            yearBox.SendKeys("2000");

            var maleBox = driver.FindElement(By.CssSelector("div:nth-of-type(1) > .fcw.fcw-radio > .d-flex-ac.radio-wrapper"));
            maleBox.Click();

            driver.FindElement(By.CssSelector("form > .btn.btn-action.undefined")).Click();

            var emailBox = driver.FindElement(By.Name("email"));
            emailBox.SendKeys(email);

            driver.FindElement(By.CssSelector(".auth-page__body")).Click();

            string expectedMessage = "Имейлът трябва да е с минимална дължина 5 символа";
            var errorMessage = driver.FindElement(By.CssSelector(".bg--error.d-flex-ac.error.form-message.w-100 > .flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));

        }
        [TestCase("11111")]
        [TestCase("aaaaaa")]
        [TestCase("@#$%^&")]
        [TestCase("email@abv")]
        [TestCase("@abv.bg")]
        [TestCase("valid mail@abv.bg")]
        public void Test_CreateNewUser_WithInvalidEmail(string email)
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.CssSelector(".btn.btn-primary.ml-auto")).Click();

            var firstNameBox = driver.FindElement(By.Name("firstName"));
            firstNameBox.SendKeys("FirstName");

            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.SendKeys("LastName");

            var dayBox = driver.FindElement(By.Name("day"));
            dayBox.SendKeys("1");

            var monthBox = driver.FindElement(By.Name("month"));
            monthBox.SendKeys("1");

            var yearBox = driver.FindElement(By.Name("year"));
            yearBox.SendKeys("2000");

            var maleBox = driver.FindElement(By.CssSelector("div:nth-of-type(1) > .fcw.fcw-radio > .d-flex-ac.radio-wrapper"));
            maleBox.Click();

            driver.FindElement(By.CssSelector("form > .btn.btn-action.undefined")).Click();

            var emailBox = driver.FindElement(By.Name("email"));
            emailBox.SendKeys(email);

            driver.FindElement(By.CssSelector(".auth-page__body")).Click();

            string expectedMessage = "Въведеният имейл е невалиден";
            var errorMessage = driver.FindElement(By.CssSelector(".bg--error.d-flex-ac.error.form-message.w-100 > .flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));
        }
                
        [TestCase("1")]        
        public void Test_CreateNewUser_WithInvalidPhoneNumber_LessThen3Numbers(string phone)
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.CssSelector(".btn.btn-primary.ml-auto")).Click();

            var firstNameBox = driver.FindElement(By.Name("firstName"));
            firstNameBox.SendKeys("FirstName");

            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.SendKeys("LastName");

            var dayBox = driver.FindElement(By.Name("day"));
            dayBox.SendKeys("1");

            var monthBox = driver.FindElement(By.Name("month"));
            monthBox.SendKeys("1");

            var yearBox = driver.FindElement(By.Name("year"));
            yearBox.SendKeys("2000");

            var maleBox = driver.FindElement(By.CssSelector("div:nth-of-type(1) > .fcw.fcw-radio > .d-flex-ac.radio-wrapper"));
            maleBox.Click();

            driver.FindElement(By.CssSelector("form > .btn.btn-action.undefined")).Click();

            var phoneBox = driver.FindElement(By.CssSelector(".input-group.react-tel-input.rti > .form-control"));
            phoneBox.SendKeys(phone);

            driver.FindElement(By.CssSelector(".auth-page__body")).Click();

            string expectedMessage = "Телефонният номер трябва да бъде минимум 3 символа";
            var errorMessage = driver.FindElement(By.CssSelector(".bg--error.d-flex-ac.error.form-message.w-100 > .flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));

        }
        [TestCase("")]                
        public void Test_CreateNewUser_WithInvalidPhoneNumber_EmptyString(string phone)
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.CssSelector(".btn.btn-primary.ml-auto")).Click();

            var firstNameBox = driver.FindElement(By.Name("firstName"));
            firstNameBox.SendKeys("FirstName");

            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.SendKeys("LastName");

            var dayBox = driver.FindElement(By.Name("day"));
            dayBox.SendKeys("1");

            var monthBox = driver.FindElement(By.Name("month"));
            monthBox.SendKeys("1");

            var yearBox = driver.FindElement(By.Name("year"));
            yearBox.SendKeys("2000");

            var maleBox = driver.FindElement(By.CssSelector("div:nth-of-type(1) > .fcw.fcw-radio > .d-flex-ac.radio-wrapper"));
            maleBox.Click();

            driver.FindElement(By.CssSelector("form > .btn.btn-action.undefined")).Click();

            var phoneBox = driver.FindElement(By.CssSelector(".input-group.react-tel-input.rti > .form-control"));
            phoneBox.SendKeys(phone);

            driver.FindElement(By.CssSelector(".auth-page__body")).Click();

            string expectedMessage = "Полето за телефонен номер е задължително";
            var errorMessage = driver.FindElement(By.CssSelector(".bg--error.d-flex-ac.error.form-message.w-100 > .flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));

        }
        [TestCase("222")]
        public void Test_CreateNewUser_WithInvalidPhoneNumber_ExistingNumber(string phone)
        {
            driver.Navigate().GoToUrl(url);
            driver.FindElement(By.CssSelector(".btn.btn-primary.ml-auto")).Click();

            var firstNameBox = driver.FindElement(By.Name("firstName"));
            firstNameBox.SendKeys("FirstName");

            var familyNameBox = driver.FindElement(By.Name("lastName"));
            familyNameBox.SendKeys("LastName");

            var dayBox = driver.FindElement(By.Name("day"));
            dayBox.SendKeys("1");

            var monthBox = driver.FindElement(By.Name("month"));
            monthBox.SendKeys("1");

            var yearBox = driver.FindElement(By.Name("year"));
            yearBox.SendKeys("2000");

            var maleBox = driver.FindElement(By.CssSelector("div:nth-of-type(1) > .fcw.fcw-radio > .d-flex-ac.radio-wrapper"));
            maleBox.Click();

            driver.FindElement(By.CssSelector("form > .btn.btn-action.undefined")).Click();

            var phoneBox = driver.FindElement(By.CssSelector(".input-group.react-tel-input.rti > .form-control"));
            phoneBox.SendKeys(phone);

            driver.FindElement(By.CssSelector(".auth-page__body")).Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5); 

            string expectedMessage = "Телефонният номер вече съществува";
            var errorMessage = driver.FindElement(By.CssSelector(".bg--error.d-flex-ac.error.form-message.w-100 > .flex-auto"));

            Assert.That(errorMessage.Text, Is.EqualTo(expectedMessage));
        }


            [TearDown]
        public void CloseBrowser()
        {
            this.driver.Quit();
        }
    }
}