using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Services;
using ExpenseTracker.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddBlazorBootstrap();

// TODO: Refactor this code
builder.Services.AddHttpClient<IExpensesService, ExpensesService>(client =>
{
    var apiUrl = "https://localhost:7290";
    /*
    var apiUrl = builder.Configuration["ApiUrl"] ??
                 throw new InvalidOperationException("Configuration value 'ApiUrl' not found.");
                 */
    client.BaseAddress = new Uri(apiUrl);
});

builder.Services.AddHttpClient<IExpenseCategoriesService, ExpenseCategoriesService>(client =>
{
    var apiUrl = "https://localhost:7290";
    /*
    var apiUrl = builder.Configuration["ApiUrl"] ??
                 throw new InvalidOperationException("Configuration value 'ApiUrl' not found.");
                 */
    client.BaseAddress = new Uri(apiUrl);
});

builder.Services.AddHttpClient<IExpenseTagsService, ExpenseTagsService>(client =>
{
    var apiUrl = "https://localhost:7290";
    /*
    var apiUrl = builder.Configuration["ApiUrl"] ??
                 throw new InvalidOperationException("Configuration value 'ApiUrl' not found.");
                 */
    client.BaseAddress = new Uri(apiUrl);
});

await builder.Build().RunAsync();
