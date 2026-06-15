using Microsoft.Playwright;
using NUnit.Framework.Internal;
public class LoginPage
{
    private readonly IPage page;

    public LoginPage(IPage page)
    {
        this.page = page;
    }

    private ILocator Username => page.Locator("input[placeholder='Username']");
    private ILocator Password => page.Locator("input[placeholder='Password']");
    private ILocator LoginButton => page.Locator("input[type='submit']#login-button");
    private ILocator ErrorMessage => page.Locator("//h3[contains(text(), 'Epic sadface: Username and password do not match any user in this service')]");
    private ILocator ErrorMessageLocked => page.Locator("//h3[contains(text(), 'Epic sadface: Sorry, this user has been locked out.')]");
    private ILocator BurgerMenuButton => page.Locator("//button[@id='react-burger-menu-btn']");
    private ILocator LogoutButton => page.Locator("//a[@id='logout_sidebar_link']");

    public async Task LoginAs(string user, string pass)
    {
        await Username.FillAsync(user);
        await Password.FillAsync(pass);
        await LoginButton.ClickAsync();
    }

        public async Task<bool> IsErrorVisible()
    {
        return await ErrorMessage.IsVisibleAsync();
    }

    public async Task<bool> IsLockedOutErrorVisible()
    {
        return await ErrorMessageLocked.IsVisibleAsync();
        
    }

    public async Task Logout()
    {
        await BurgerMenuButton.ClickAsync();
        await LogoutButton.WaitForAsync();
        await LogoutButton.ClickAsync();
    }
}