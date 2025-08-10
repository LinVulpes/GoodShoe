using GoodShoe.Data;
using GoodShoe.Services;
using Microsoft.AspNetCore.Mvc.Razor; //so ICartService/CartService are in scope
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the Container.
builder.Services.AddControllersWithViews();

// Account authentication
builder.Services.AddScoped<IAuthService, AuthService>();

// Session configuration - updated with timeout 30 mins
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Path for any action of Product Management in AdminController to looks in Views/Admin/Product_Management/
builder.Services.Configure<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Add("/Views/Admin/Product_Management/{0}.cshtml");
});

// Make IHttpContextAccessor available (needed by CartService)
builder.Services.AddHttpContextAccessor();

// Register your cart service
builder.Services.AddScoped<ICartService, CartService>();
// Commented for now -> builder.Services.AddScoped<IAuthService, AuthService>();

// Added Entity Framework
builder.Services.AddDbContext<GoodShoeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GoodShoeContext"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Initializing database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        DbInitializer.Initialize(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error initializing database: {ex.Message}");
    }
}

app.Run();