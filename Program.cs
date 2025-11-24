using EficiaBackend.Data;
using Microsoft.EntityFrameworkCore;
using EficiaBackend.Services;
using EficiaBackend.Services.Interfaces;
using EficiaBackend.Repositories;
using EficiaBackend.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// ===== DATABASE =====
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString,ServerVersion.AutoDetect(connectionString))
);


// ===== CONTROLLERS & OPENAPI =====
builder.Services.AddControllers();
builder.Services.AddOpenApi();

// ===== DEPENDENCY INJECTION =====
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtService, JwtService>();

<<<<<<< HEAD
builder.Services.AddScoped<INoteRepository,NoteRepository>();
builder.Services.AddScoped<INoteService,NoteService>();
=======

builder.Services.AddScoped<IUserStatsRepository,UserStatsRepository>();
builder.Services.AddScoped<IUserStatsService,UserStatsService>();
>>>>>>> origin/feature/statistics

// ===== JWT CONFIGURATION =====
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = jwtSettings["Key"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key)
        )
    };
});

// ===== BUILD =====
var app = builder.Build();

// ===== HTTP PIPELINE =====
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();
