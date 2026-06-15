using Microsoft.Playwright;
using NUnit.Framework.Internal;

public class CheckoutPage
{
    private readonly IPage page;
    private ILocator pageTitle => page.Locator("//span[@class='title' and text()='Checkout: Your Information']");
    private ILocator cartButton => page.Locator("//span[@class='shopping_cart_badge']");
    private ILocator checkoutButton => page.Locator("//button[@id='checkout']");
    private ILocator firstNameInput => page.Locator("//input[@id='first-name']");
    private ILocator lastNameInput => page.Locator("//input[@id='last-name']");
    private ILocator postalCodeInput => page.Locator("//input[@id='postal-code']");
    private ILocator continueButton => page.Locator("//input[@id='continue']");
    private ILocator checkoutOverviewPage => page.Locator("//span[@class='title' and text()='Checkout: Overview']");
    private ILocator finishButton => page.Locator("//button[@id='finish']");
    private ILocator checkoutCompletePage => page.Locator("//h2[contains(text(), 'Thank you for your order!')]");



    public CheckoutPage(IPage page)
    {
        this.page = page;
    }

    public async Task<bool> IsLoaded()
    {
        return await pageTitle.IsVisibleAsync();
    }

    public async Task GoToCart()
    {
        await cartButton.ClickAsync();
    }

    public async Task GoToCheckout()
    {
        await checkoutButton.ClickAsync();
    }

    public async Task FillCheckoutInfo(string firstName, string lastName, string postalCode)
    {
        await firstNameInput.FillAsync(firstName);
        await lastNameInput.FillAsync(lastName);
        await postalCodeInput.FillAsync(postalCode);
        await continueButton.ClickAsync();
    }

    public async Task FinishCheckout()
    {
        await checkoutOverviewPage.IsVisibleAsync();
        await finishButton.ClickAsync();
        await checkoutCompletePage.IsVisibleAsync();
    }


}