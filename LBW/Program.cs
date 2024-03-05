using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
var connStr = builder.Configuration.GetConnectionString("CadenaSQL");

builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


// Add services to the container.
builder.Services
    .AddDbContext<LBW.Models.Entity.LbwContext>(options =>
    { object value = options.UseSqlServer(connStr); })
    .AddControllersWithViews()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseSession();

app.UseStaticFiles();

app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();
 

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}/{id?}");

app.Run();
