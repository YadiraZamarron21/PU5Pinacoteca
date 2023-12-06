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


//INYECTANDO EL CONTEXT
builder.Services.AddDbContext<PinacotecabdContext>
    (x=>x.UseMySql("server=localhost;user=root;password=root;database=pinacotecabd", 
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql")));

builder.Services.AddMvc();
var app = builder.Build();
app.UseFileServer();

//ESTE PARA RUTEAR LAS AREAS
app.MapControllerRoute(
     name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

app.MapDefaultControllerRoute();
app.Run();
