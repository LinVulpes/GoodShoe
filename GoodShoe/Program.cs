using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GoodShoe.Data;

var builder = WebApplication.CreateBuilder(args);

// Added Entity Framework
builder.Services.AddDbContext<GoodShoeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GoodShoeContext"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()));

// Add Identity services
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
    {
        // Configure password requirements
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    
        // Configure sign-in requirements
        options.SignIn.RequireConfirmedAccount = false;
        options.SignIn.RequireConfirmedEmail = false;
    })
.AddEntityFrameworkStores<GoodShoeContext>();

// Configure application cookie
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// MVC services
builder.Services.AddControllersWithViews();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

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