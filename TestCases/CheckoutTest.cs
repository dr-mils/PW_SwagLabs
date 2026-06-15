using FluentAssertions;
using Microsoft.Playwright;

public class CheckoutTest : TestBase
{
    [Test]
    public async Task E2ETest()
    {
        var login = new LoginPage(Page);
        var inventory = new InventoryPage(Page);
        var cart = new CartPage(Page);
        var checkout = new CheckoutPage(Page);

        await login.LoginAs("standard_user", "secret_sauce");
        (await inventory.IsLoaded()).Should().BeTrue();

        await inventory.AddRandomItemToCart();
        await Task.Delay(1000);
        await inventory.CheckCartBtn("1");
        await inventory.AddRandomItemToCart();
        await Task.Delay(1000);
        await inventory.CheckCartBtn("2");

        await checkout.GoToCart();
        (await cart.IsLoaded()).Should().BeTrue();
        await checkout.GoToCheckout();
        (await checkout.IsLoaded()).Should().BeTrue();

        await Task.Delay(5000);
        await checkout.FillCheckoutInfo("John", "Doe", "12345");
        await checkout.FinishCheckout();
        await Task.Delay(5000);
    }
}
