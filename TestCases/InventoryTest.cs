using FluentAssertions;
using Microsoft.Playwright;

public class InventoryTests : TestBase
{
    [Test]
    public async Task InventoryPageLoadsAfterLogin()
    {
        var login = new LoginPage(Page);
        var inventory = new InventoryPage(Page);

        await login.LoginAs("standard_user", "secret_sauce");
        (await inventory.IsLoaded()).Should().BeTrue();

        await Task.Delay(5000);
    }

    [Test]
    public async Task AddingSingleRandomItemToCart()
    {
        var login = new LoginPage(Page);
        var inventory = new InventoryPage(Page);
        var cart = new CartPage(Page);

        await login.LoginAs("standard_user", "secret_sauce");
        (await inventory.IsLoaded()).Should().BeTrue();
        await inventory.AddRandomItemToCart();

        await inventory.CheckCartBtn("1");

        await Task.Delay(5000);
    }

    [Test]
    public async Task AddTwoItems()
    {
        var login = new LoginPage(Page);
        var inventory = new InventoryPage(Page);
        var cart = new CartPage(Page);

        await login.LoginAs("standard_user", "secret_sauce");
        (await inventory.IsLoaded()).Should().BeTrue();
        await inventory.AddRandomItemToCart();
        await Task.Delay(1000);
        await inventory.AddRandomItemToCart();
        await Task.Delay(1000);
        await inventory.CheckCartBtn("2");

        await Task.Delay(5000);
    }

    [Test]
    public async Task DeleteOneItemFromCart()
    {
        var login = new LoginPage(Page);
        var inventory = new InventoryPage(Page);
        var cart = new CartPage(Page);

        await login.LoginAs("standard_user", "secret_sauce");
        (await inventory.IsLoaded()).Should().BeTrue();
        await inventory.AddRandomItemToCart();
        await Task.Delay(1000);
        await inventory.CheckCartBtn("1");
        await inventory.AddRandomItemToCart();
        await Task.Delay(1000);
        await inventory.CheckCartBtn("2");

        await inventory.RemoveRandomItemFromCart();

        await Task.Delay(5000);
    }

    [Test]
    public async Task SortItems()
    {
        var login = new LoginPage(Page);
        var inventory = new InventoryPage(Page);

        await login.LoginAs("standard_user", "secret_sauce");
        (await inventory.IsLoaded()).Should().BeTrue();

        await inventory.SortItems("Price (low to high)");
        await Task.Delay(5000);
    }
}