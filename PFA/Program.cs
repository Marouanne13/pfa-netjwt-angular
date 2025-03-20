using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using PFA.Data;
using PFA.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<MessageService>();

builder.Services.AddScoped<TransportService>();

// 📌 Configuration de la base de données SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Durée de session (30 min ici)
    options.Cookie.HttpOnly = true; // Sécuriser les cookies
    options.Cookie.IsEssential = true; // Nécessaire pour fonctionner même si l'utilisateur refuse les cookies non essentiels
});


// 📌 Configuration de CORS (pour autoriser Angular)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

// 📌 Configuration de l'authentification JWT
var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "CeciEstUneSuperCleSecreteJWT123456789");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false, // Désactive si l'`Issuer` n'est pas défini
            ValidateAudience = false, // Désactive si l'`Audience` n'est pas définie
        };

        // Gestion des erreurs JWT
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"🚨 JWT Authentication Failed: {context.Exception.Message}");
                return Task.CompletedTask;
            }
        };
    });

builder.Services.AddAuthorization();
builder.Services.AddControllers();

// 📌 Configuration Swagger pour inclure un bouton "Authorize"
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PFA API", Version = "v1" });

    // 🔹 Ajout du bouton "Authorize" pour le JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Entrer le token JWT au format **'Bearer YOUR_TOKEN'**"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// 📌 Activation de Swagger uniquement en mode développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PFA API v1");
        c.RoutePrefix = ""; // Accès direct à Swagger via http://localhost:5278/
    });
}

app.UseRouting();
app.UseSession();
app.UseCors("AllowAll"); // ✅ Activation de CORS ici
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
