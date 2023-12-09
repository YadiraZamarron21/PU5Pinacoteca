using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using PU5Pinacoteca.Models.Entities;
using PU5Pinacoteca.Repositories;

var builder = WebApplication.CreateBuilder(args);
//INYECTAR EL REPOSITORIO
builder.Services.AddTransient<Repository<Coleccion>>();
builder.Services.AddTransient<PintoresRepository>();
builder.Services.AddTransient<CuadrosRepository>();
builder.Services.AddTransient<Repository<Pintor>>();
builder.Services.AddTransient<Repository<Usuarios>>();

//AUTENTICACION
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x =>
{
    x.AccessDeniedPath = "/Home/Denied"; //ACCESO DENEGADO
    x.LoginPath = "/Home/Login"; //ACCESO PERMITIFO
    x.LogoutPath = "/Home/Logout"; //CERRAR SESION
    x.ExpireTimeSpan = TimeSpan.FromMinutes(30); //COOKIE ACTIVA
    x.Cookie.Name = "pinacotecaCookie"; //NOMBRE COOKIE

});

//INYECTANDO EL CONTEXT
builder.Services.AddDbContext<PinacotecabdContext>
    (x=>x.UseMySql("server=websitos256.com;user=websitos_Pinacoteca;password=1i35%e4gS;database=websitos_Pinacoteca", 
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql")));

builder.Services.AddMvc();
var app = builder.Build();
app.UseFileServer();

//ESTE PARA RUTEAR LAS AREAS
app.MapControllerRoute(
     name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.Run();
