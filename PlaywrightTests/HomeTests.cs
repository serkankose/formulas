using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
public class Tests : PageTest
{
    [Test]
    public async Task HomePageNavLinksTest()
    {
        await Page.GotoAsync("https://localhost:7296");

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Formulas"));

        await TestNavLink("Banking");
        await TestNavLink("Corporate");
        await TestNavLink("Financial");
        await TestNavLink("Financial Markets");
        await TestNavLink("General Finance");
        await TestNavLink("Stocks and Bonds");
    }

    private async Task TestNavLink(string name, int index = 0)
    {
        // create a locator
        // var locator = Page.Locator($":nth-match(:text('{name}'), {index})");
        var locator = Page.Locator($".nav-link:has-text(\"{name}\")").Nth(index);
        //Assert.AreEqual(1, await locator.CountAsync());

        // Expect an attribute "to be strictly equal" to the value.
        var nameLowerNoSpaces = name.ToLower().Replace(" ", "");
        await Expect(locator).ToHaveAttributeAsync("href", $"{nameLowerNoSpaces}");

        // Click the get started link.
        await locator.ClickAsync();

        // Expects the URL to contain intro.
        await Expect(Page).ToHaveURLAsync(new Regex($".*{nameLowerNoSpaces}"));
    }

    [Test]
    public async Task CalcAnnualPercentageYieldTest()
    {
        await Page.GotoAsync("https://localhost:7296/banking");

        await CheckCalculationResult(
            "CalcAnnualPercentageYield",
            new Dictionary<string, string>
            {
                { "statedAnnualInterestRate", "2" },
                { "numberOfTimesCompounded", "5" },
            },
            "4.37824");
    }
    [Test]
    public async Task CalcRealRateOfReturn()
    {
        await Page.GotoAsync("https://localhost:7296/financial");

        await CheckCalculationResult(
            "CalcUnitsOfProduction",
            new Dictionary<string, string>
            {
                { "costOfAsset", "2" },
                { "residualValue", "3" },
                { "estimatedTotalProduction", "4" },
                { "actualProduction", "5" },
            },
            "-1.25");
    }

    private async Task CheckCalculationResult(string methodName, Dictionary<string, string> paramsDict, string expected)
    {
        var method = Page.Locator($"#{methodName}");

        foreach (var paramValue in paramsDict)
        {
            var param01 = method.Locator($"input:right-of(:text(\"{paramValue.Key}\"))").Nth(0);
            await param01.FillAsync(paramValue.Value);
        }

        var result = method.Locator($"id={methodName.ToLower()}_result");
        await Expect(result).ToHaveTextAsync(expected);
    }
}