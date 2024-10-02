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

var apiUrl = builder.Configuration["ExpenseTrackerApi:BaseUrl"] ?? 
             throw new InvalidOperationException("Configuration value 'ApiUrl' not found.");

builder.Services.AddHttpClient<IExpensesService, ExpensesService>(c => c.BaseAddress = new Uri(apiUrl));
builder.Services.AddHttpClient<IExpenseCategoriesService, ExpenseCategoriesService>(c => c.BaseAddress = new Uri(apiUrl));
builder.Services.AddHttpClient<IExpenseTagsService, ExpenseTagsService>(c => c.BaseAddress = new Uri(apiUrl));
builder.Services.AddHttpClient<IStatisticsService, StatisticsService>(client => client.BaseAddress = new Uri(apiUrl));
builder.Services.AddHttpClient<IIncomesService, IncomesService>(client => client.BaseAddress = new Uri(apiUrl));
builder.Services.AddHttpClient<IIncomeCategoriesService, IncomeCategoriesService>(client => client.BaseAddress = new Uri(apiUrl));

builder.Services.AddHttpClient<IReceiptsService, ReceiptsService>(client =>
{
    var receiptsApiUrl = builder.Configuration["ReceiptsApi:BaseUrl"] ?? 
                 throw new InvalidOperationException("Configuration value 'ReceiptsApi:BaseUrl' not found.");
    client.BaseAddress = new Uri(receiptsApiUrl);
});

await builder.Build().RunAsync();
