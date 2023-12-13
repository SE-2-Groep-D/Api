using Api.Data;
using Api.Models.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AccessibilityPanelAPI; 
public class Program { 
    public static void Main(string[] args) {
            // configure application
            var builder = WebApplication.CreateBuilder(args);
            SetupServices(builder.Services, builder);
            
            // add middelware
            var app = builder.Build();
            SetupMiddleware(app);
            
            // run application
            app.Run();
    }

    private static void SetupServices(IServiceCollection services, WebApplicationBuilder builder) { 
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<AccessibilityDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("APIDbConnectionString")));


        services.AddIdentityCore<Gebruiker>()
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<Gebruiker>>("API")
            .AddEntityFrameworkStores<AccessibilityDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;
        });

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            });
    }
    private static void SetupMiddleware(WebApplication app) {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        
        app.MapControllers();
    }
}