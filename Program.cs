using BibliotecaMVC.Interfaces;
using BibliotecaMVC.Repositories;
using BibliotecaMVC.Services;
using Microsoft.EntityFrameworkCore;
using BibliotecaMVC.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BibliotecaContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRepositorioLibros, RepositorioLibrosEnMemoria>();
builder.Services.AddScoped<IRepositorioAutores, RepositorioAutoresEnMemoria>();

// Servicios
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<ILibrosService, LibrosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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