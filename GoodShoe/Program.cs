using Microsoft.EntityFrameworkCore;
using GoodShoe.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the Container.
builder.Services.AddControllersWithViews();

// Added Entity Framework
builder.Services.AddDbContext<GoodShoeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("GoodShoeContext"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()));

var app = builder.Build();

// Initializing database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    DbInitializer.Initialize(services);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();