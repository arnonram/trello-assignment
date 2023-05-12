# Arnon's Knab Assignement

The project has two tests, an API test using RestSharp as the REST API Client, and UI test using [Playwright](https://playwright.dev/dotnet/)

## Setup

1. Start by running:

```bash
dotnet build
```

2. Update the `appsettings.json` file with appropriate data

---

</br>

## API Tests

To run the API tests, run:

```bash
dotnet test --filter "CreateBoardApiTests"
```

---

</br>

## Front End Tests

Playwright needs it's own browser binaries in order to run smoothly. To make things easier this is done programmaticaly via a function in `Utils/InstallPlaywrightBrowsers.cs`

| FLAG                                                                       | EXPLANATION                                                                                                          |
| -------------------------------------------------------------------------- | -------------------------------------------------------------------------------------------------------------------- |
| `PLAYWRIGHT_DEPS=1`                                                        | If you do not have the playwright borwser binaries on your env, you will need to run the test command with this flag |
| `HEADED=1`                                                                 | By default, playwright runs headless. In order to run tests headed you will need to add this flag                    |
| `-- Playwright.BrowserName=firefox`<br> `-- Playwright.BrowserName=webkit` | By default **_chromium_** is used for tests. To change that add one of these to the command                          |

### Examples

```bash
# Base command to run with installing any browser deps and run headless
dotnet test --filter "FrontEndTests"

# Install browser deps during test run and run headless
PLAYWRIGHT_DEPS=1 dotnet test --filter "FrontEndTests"

# Do not install browsers deps during test run and run Firefox headed
HEADED=1 dotnet test --filter "FrontEndTests" -- Playwright.BrowserName=firefox
```
