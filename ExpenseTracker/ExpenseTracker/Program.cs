using ExpenseTracker.Business.Client.Abstraction;
using ExpenseTracker.Business.Client.Services;
using ExpenseTracker.Business.Expenses.Queries;
using ExpenseTracker.Components;
using ExpenseTracker.Components.Account;
using ExpenseTracker.Data.DbContext;
using ExpenseTracker.Data.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add MediatR
builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<GetExpensesQuery>());

builder.Services.AddBlazorBootstrap();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

// Seed database
// builder.Services.AddTransient<DatabaseSeeder>();

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

builder.Services.AddHttpClient<IStatisticsService, StatisticsService>(client =>
{
    var apiUrl = "https://localhost:7290";
    /*
    var apiUrl = builder.Configuration["ApiUrl"] ??
                 throw new InvalidOperationException("Configuration value 'ApiUrl' not found.");
                 */
    client.BaseAddress = new Uri(apiUrl);
});

builder.Services.AddHttpClient<IIncomesService, IncomesService>(client =>
{
    var apiUrl = "https://localhost:7290";
    /*
    var apiUrl = builder.Configuration["ApiUrl"] ??
                 throw new InvalidOperationException("Configuration value 'ApiUrl' not found.");
                 */
    client.BaseAddress = new Uri(apiUrl);
});

builder.Services.AddHttpClient<IIncomeCategoriesService, IncomeCategoriesService>(client =>
{
    var apiUrl = "https://localhost:7290";
    /*
    var apiUrl = builder.Configuration["ApiUrl"] ??
                 throw new InvalidOperationException("Configuration value 'ApiUrl' not found.");
                 */
    client.BaseAddress = new Uri(apiUrl);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
    
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Seed database
    // using var serviceScope = app.Services.CreateScope();
    // var seeder = serviceScope.ServiceProvider.GetService<DatabaseSeeder>();
    // seeder.Seed();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(ExpenseTracker.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapControllers();

app.Run();
