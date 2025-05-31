using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace TestCatShelter;

public class SeleniumTest : IDisposable
{
    private readonly IWebDriver driver;
    public SeleniumTest()
    {
        driver = new FirefoxDriver();
    }
    public void Dispose()
    {
        driver.Quit();
        driver.Dispose();
    }
    [Fact]
    public void CreateCatCheckIfInIndex()
    {
        var name = Guid.NewGuid().ToString();
        driver.Navigate().GoToUrl("http://localhost:5272/Cat/Create");
        Assert.Equal("Create - Cat Shelter Admin", driver.Title);

        driver.FindElement(By.Id("Name")).SendKeys(name);
        driver.FindElement(By.Id("YearOfBirth")).Clear();
        driver.FindElement(By.Id("YearOfBirth")).SendKeys("1920");
        driver.FindElement(By.Id("ArriveDate")).Clear();
        driver.FindElement(By.Id("ArriveDate")).SendKeys("2005-04-02");
        driver.FindElement(By.Id("Picture")).SendKeys("catPics");
        driver.FindElement(By.Id("submit-button")).Click();

        driver.Navigate().GoToUrl("http://localhost:5272/Cat");
        Assert.Contains(name, driver.PageSource);
    }
}