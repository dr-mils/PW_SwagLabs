using FluentAssertions;

public class LoginTests : TestBase
{
    [Test]
    public async Task SuccessfulLogin()
    {
        var login = new LoginPage(Page);
        var inventory = new InventoryPage(Page);

        await login.LoginAs("standard_user", "secret_sauce");

        (await inventory.IsLoaded()).Should().BeTrue();

        await Task.Delay(5000);
    }

    [Test]
    public async Task InvalidLoginShowsError()
    {
        var login = new LoginPage(Page);

        await login.LoginAs("wrong_user", "wrong_pass");

        (await login.IsErrorVisible()).Should().BeTrue();

        await Task.Delay(5000);
    }

    [Test]
    public async Task LoginAttemptWithLockedOutUser()
    {
        var login = new LoginPage(Page);

        await login.LoginAs("locked_out_user", "secret_sauce");
        (await login.IsLockedOutErrorVisible()).Should().BeTrue();

        await Task.Delay(5000);
    }

    [Test]
    public async Task LogoutAfterSuccessfulLogin()
    {
        var login = new LoginPage(Page);
        var inventory = new InventoryPage(Page);

        await login.LoginAs("standard_user", "secret_sauce");
        (await inventory.IsLoaded()).Should().BeTrue();

        await login.Logout();

        (await login.IsErrorVisible()).Should().BeFalse();

        await Task.Delay(5000);
    }
}