using NetCoreLinqToSqlInjection.Controllers;
using NetCoreLinqToSqlInjection.Models;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//RESOLVEMOS EL SERVICIO COCHE
//builder.Services.AddTransient<Coche>();
//builder.Services.AddSingleton<ICoche, Deportivo>();
Coche car = new Coche();
car.Marca = "Cupra";
car.Modelo = "Leon";
car.Imagen = "cupra2.jpg";
car.Velocidad = 0;
car.VelocidadMaxima = 280;
//PARA ENVIAR NUESTRO OBJETO PERSONALIZADO SE UTILIZA LAMBDA
builder.Services.AddSingleton<ICoche, Coche>(x => car);

var app = builder.Build();

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
