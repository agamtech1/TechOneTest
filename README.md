# Number to Words (ASP.NET Core / C#)

Converts a dollar amount (e.g., `123.45`) into uppercase English words  
(e.g., `ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS`).

- **Web UI:** ASP.NET Core Razor Pages (`TechOne Test`)
- **Core logic:** C# Class Library (`NumberToWords`)
- **Tests:** xUnit (`NumberToWords.Tests`)

---

## Requirements
- **.NET 8 SDK**
- Windows 10/11
- (Recommended) **Visual Studio 2022** with the *ASP.NET and web development* workload

---

## Build

### Visual Studio
1. Open `TechOneTest.sln`.
2. **Build → Rebuild Solution**.

### CLI
```bash
dotnet build
```


## Run

### Visual Studio

1. Right-click TechOne Test → Set as Startup Project.

2. Choose IIS Express or TechOne Test in the toolbar.

3. Press F5.

4. Browser opens at https://localhost:<port>/ (or http://localhost:<port>/).

### CLI

From the project folder (TechOneTest):

```bash
dotnet run
```

You’ll see something like:

Now listening on: https://localhost:<port>
Now listening on: http://localhost:<port>

### Interact

1. Open the localhost URL printed on startup.

2. Enter an amount (e.g., 123.45) -> Convert.

3. The result displays in uppercase words. The input only accepts non-negative numbers with up to two decimals.

### Testing

## Visual Studio

Test -> Test Explorer -> Run All

## CLI

```bash
dotnet test
```