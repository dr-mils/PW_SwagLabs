using Microsoft.Playwright;
using NUnit.Framework.Internal;

public class CartPage
{
    private readonly IPage page;

    public CartPage(IPage page)
    {
        this.page = page;
    }

    private ILocator pageTitle => page.Locator("//*[@class='title' and text()='Your Cart']");

    public async Task<bool> IsLoaded()
    {
        return await pageTitle.IsVisibleAsync();
    }
}