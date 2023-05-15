# Arnon's Knab Assignment

The project has two tests, an API test using [RestSharp](https://restsharp.dev/) as the REST API Client, and UI test using [Playwright](https://playwright.dev/dotnet/)

---

<br>

## Setup

1. Start by running:

```bash
dotnet restore
dotnet build
```

2. Update the `Tests/appsettings.json` file with appropriate data

---

</br>

## API Tests

To run the API tests, run:

```bash
dotnet test Tests --filter "CreateBoardApiTests"
```

---

</br>

## Front End Tests

Playwright needs it's own browser binaries in order to run smoothly. To make things easier this is done programmaticaly via a function in `Utils/InstallPlaywrightBrowsers.cs`

| FLAG                                                                      | EXPLANATION                                                                                                          |
| ------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------- |
| `PLAYWRIGHT_DEPS=1`                                                       | If you do not have the playwright borwser binaries on your env, you will need to run the test command with this flag |
| `HEADED=1`                                                                | By default, playwright runs headless. In order to run tests headed you will need to add this flag                    |
| `-- Playwright.BrowserName=firefox`<br>`-- Playwright.BrowserName=webkit` | By default **_chromium_** is used for tests. To change that add one of these to the command                          |

### Examples

```bash
# Base command to run without installing any browser deps and run headless
dotnet test Tests --filter "FrontEndTests"

# Install browser deps during test run and run headless
PLAYWRIGHT_DEPS=1 dotnet test Tests --filter "FrontEndTests"

# Do not install browsers deps during test run and run Firefox headed
HEADED=1 dotnet test Tests --filter "FrontEndTests" -- Playwright.BrowserName=firefox
```

---

<br>

# Future Improvements

- These couple of tests only perform "happy flows". Of course, adding negative tests is a must.
- Add better error handling to the API Client. This will allow us to better test failures, and verify we receive correct error messages.
- Adding a propper logger (in the past I used Serilog). In testing I like to add lots of logs (especially in API testing), for any action or API call that is performed, what is sent, what is received, etc... If the test fails, as a tester I like to know exaclty what happened, and what data was used.
- Some of the `Setup` and `Teardown` steps in both API and Front End tests would be moved to a more global Setup/Teardown file. We will not want to repeat these steps everytime once we have lots of tests.
- Ideally, these two kinds of tests would be in separate projects. But if that would be the case, then the `ApiClient` would probably also become an independant project, as there are many times where some setup or teardown done via API would save a lot of time for Front Side testing (for example: creating many users, or borads with data, before even running the Client test).
- For the Playwright tests, when we would have multiple tests, it will be more time efficient to log-in once, and save the cockies and/or browser storage for all tests.
- Mocking:
  - For API tests: while we would usually not want to mock anything, and verify that the system is running correctly with all services communicating correctly, we might consider mocking areas that are heavy and take time to run (for example, testing trello boards with a lot of data)
  - For Front End tests: Most modern test UI frameowrk support API mocking as well. In some cases, this might be useul in order to run quicker more stable tests.
- The `InstallPlaywrightBrowsers` is needed mainly for this specific use case. If this was a real project, then every developer will need to install the deps themselves. It _might_ be usefull for CI/CD purposes, but most CI/CD pipelines run on a docker already contain all deps, so again, a very specific use case.
