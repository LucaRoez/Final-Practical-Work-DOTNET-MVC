using Microsoft.EntityFrameworkCore;
using TrabajoFinal___Bingo_Web_MVC_Service.Data;
using TrabajoFinal___Bingo_Web_MVC_Service.Services.Extensions;
using TrabajoFinal___Bingo_Web_MVC_Service.Services.Gambling;
using TrabajoFinal___Bingo_Web_MVC_Service.Data.Repository;
using TrabajoFinal___Bingo_Web_MVC_Service.Services.Extensions.ForBingo;
using TrabajoFinal___Bingo_Web_MVC_Service.Services.Gambling.ForBingo;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
var services = builder.Services;

// Entity Framework
var connectionString = config.GetConnectionString("DefaultConnection");
services.AddDbContext<BingoDbContext>(op =>
    op.UseSqlServer(connectionString));

// Inner Services
services.AddScoped<IRepository, DefaultBingoRepository>();
services.AddSingleton<System.Random>();
services.AddSingleton<IStructureConstructor, DefaultBingoConstructor>();
services.AddScoped<IDataModelExchange, BingoDataModelExchange>();
services.AddSingleton<IChanceMechanism, DrawLottery>();
services.AddScoped<IStartGamble, StartBingoGamble>();
services.AddScoped<IWager, BingoWager>();
services.AddScoped<IEndGamble, EndBingoGamble>();

services.AddControllersWithViews();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
