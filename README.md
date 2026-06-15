# Playwright-SwagLabs
Basic framework 

You need:
- .NET 10.0 or later
- Playwright browsers (automatically installed on first run)

Run all tests:
in bash: dotnet test

Tests are configured to generate traces automatically on failure.
use: playwright show-trace trace*.zip

Test results are saved to `PWTesting/TestResults/` as `.trx` files.

Project structure:
- TestBase.cs - Base class with common methods
- Pages/ - Page Object Model classes
  - LoginPage.cs - Login page interactions
  - InventoryPage.cs - Inventory page interactions
  - CheckoutPage.cs - Checkout page interactions
  - CartPage.cs - Cart page interactions
- TestCases/ - test cases
  - LoginTest.cs
  - InventoryTest.cs
  - CartTest.cs
  - CheckoutTest.cs
