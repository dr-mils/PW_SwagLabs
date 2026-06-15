using FluentAssertions;
using Microsoft.Playwright;

public class CartTests : TestBase

{
    [Test]
    public async Task AddItemToCart()
    {
        var login = new LoginPage(Page);
        var inventory = new InventoryPage(Page);
        var cart = new CartPage(Page);


        await login.LoginAs("standard_user", "secret_sauce");
        (await inventory.IsLoaded()).Should().BeTrue();

        // await inventory.AddFirstItemToCart();
        // (await cart.GetCartItemCount()).Should().Be(1);

        await Task.Delay(5000);
    }
}
