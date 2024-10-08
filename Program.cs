global using Wordle.Data;
global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Wordle.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
bool isProduction = builder.Configuration["ASPNETCORE_ENVIRONMENT"] == "Production" ? true : false;

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Configure the JS minifier only in production
if (isProduction)
{
    builder.Services.AddWebOptimizer(pipeline =>
    {
        pipeline.MinifyCssFiles("css/*.css");
        pipeline.AddCssBundle("/css/bundle.css", "css/*.css");
        pipeline.MinifyJsFiles("js/*.js");
        pipeline.AddJavaScriptBundle("/js/bundle.js", "js/*.js");
    });
}

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AuthDbContext>();builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseMySql(Environment.GetEnvironmentVariable("MYSQLCONNSTR_localdb"), new MySqlServerVersion(new Version(8, 0, 27))));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseWebOptimizer();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapRazorPages();

});

app.Run();
