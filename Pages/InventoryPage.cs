using FluentAssertions;
using Microsoft.Playwright;

public class InventoryPage
{
    private readonly IPage page;

    public InventoryPage(IPage page)
    {
        this.page = page;
    }

    private ILocator pageName => page.Locator("//span[@class='title' and text()='Products']");
    private ILocator cartButton => page.Locator("//span[@class='shopping_cart_badge']");
    private ILocator addToCartButtons => page.Locator("button.btn_inventory:has-text('Add to cart')");
    private ILocator inventoryName => page.Locator("//div[@class='inventory_item_desc']");
    private ILocator buttonRemove => page.Locator("//button[contains(text(), 'Remove')]");
    private ILocator inventorySort => page.Locator("//select[@class='product_sort_container']");

    public async Task<bool> IsLoaded()
    {
        return await pageName.IsVisibleAsync();
    }

    public async Task AddRandomItemToCart()
    {

        int count = await addToCartButtons.CountAsync();

        if (count == 0)
            throw new Exception("No Add to cart buttons found on the page.");

        var random = new Random();
        int index = random.Next(0, count);

        await addToCartButtons.Nth(index).ClickAsync();

        await Task.Delay(1000);
    }

    public async Task AddAnyItem()
    {
        await addToCartButtons.ClickAsync();
    }

    public async Task CheckCartBtn(string cartAmount)
    {
        await cartButton.IsVisibleAsync();
        (await cartButton.InnerTextAsync()).Should().Be(cartAmount);

        Console.WriteLine("Cart button is visible and shows 1 item.");
    }

    public async Task RemoveRandomItemFromCart()
    {
        int count = await buttonRemove.CountAsync();

        if (count == 0)
            throw new Exception("No Remove buttons found on the page.");

        var random = new Random();
        int index = random.Next(0, count);

        await buttonRemove.Nth(index).ClickAsync();

        await Task.Delay(1000);

        int count2 = await buttonRemove.CountAsync();

        if (count2 == 0)
        {
            Console.WriteLine("All items have been removed from the cart.");
        }
        else
        {
            Console.WriteLine($"Item removed. {count2} items remain in the cart.");
        }
    }

    public async Task SortItems(string sortOption)
    {
        await inventorySort.SelectOptionAsync(new SelectOptionValue { Label = sortOption });
        await Task.Delay(1000);
    }


}
