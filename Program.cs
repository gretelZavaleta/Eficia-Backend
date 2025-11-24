using EficiaBackend.Data; 
using EficiaBackend.Repositories;
using EficiaBackend.Repositories.Interfaces;
using EficiaBackend.Services;
using EficiaBackend.Services.Interfaces;
using Microsoft.EntityFrameworkCore; 


var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString))
);
builder.Services.AddScoped<INoteRepository,NoteRepository>();
builder.Services.AddScoped<INoteService,NoteService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
     app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


