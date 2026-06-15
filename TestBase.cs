using Microsoft.Playwright;

public class TestBase
{
    protected IPage Page;
    private IPlaywright playwright;
    private IBrowser browser;

    [SetUp]
    public async Task Setup()
    {
        playwright = await Playwright.CreateAsync();
        browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        var context = await browser.NewContextAsync();
        Page = await context.NewPageAsync();

        await context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        await Page.SetViewportSizeAsync(1920, 1080);
        await Page.GotoAsync("https://www.saucedemo.com/");
    }

    [TearDown]
    public async Task Teardown()
    {
        var testFailed = TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed;

        if (testFailed)
        {
            await Page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = $"screenshot_{TestContext.CurrentContext.Test.Name}.png"
            });

            await Page.Context.Tracing.StopAsync(new TracingStopOptions
            {
                Path = $"trace_{TestContext.CurrentContext.Test.Name}.zip"
            });
        }
        else
        {
            await Page.Context.Tracing.StopAsync();
        }

        await browser.CloseAsync();
        playwright.Dispose();
    }
}