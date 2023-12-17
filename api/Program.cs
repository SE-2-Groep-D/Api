using System.Configuration;
using Api.Data;
using Api.Services.IUserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Api.Services.ITokenService;
using Api.Mappings;
using Api.Models.Domain.User;
using Api.Repositories.IGebruikerRepository;

//using Api.Repositories.ITrackingRepository;

//using Api.Repositories.ITrackingRepository;

namespace Api;
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
    
    var connectionString = builder.Configuration.GetConnectionString("APIDbConnectionString");
    var dbType = builder.Configuration["DatabaseType"];
    
    try {
        services.AddDbContext<AccessibilityDbContext>(options => {
          switch (dbType) {
            case "sqlserver":
              options.UseSqlServer(connectionString);
              break;
            default:
              options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 6, 12)));
              break;
          }
        });
        
        Console.WriteLine("Succesfully connected to the database.");
    } catch (Exception e) {
      Console.WriteLine($"Could not connect to database: ConnectionString: {connectionString} DatabaseType: {dbType}");
      Console.WriteLine(e);
      throw;
    }


    AddRepositories(services);
    AddServices(services);

    services.AddAutoMapper(typeof(AutoMapperProfiles));

    //Usermanager voor Gebruiker instellen
    services.AddIdentityCore<Gebruiker>()
      .AddRoles<IdentityRole<Guid>>()
      .AddTokenProvider<DataProtectorTokenProvider<Gebruiker>>("API")
      .AddEntityFrameworkStores<AccessibilityDbContext>()
      .AddDefaultTokenProviders();

    //Usermanager voor Ervaringsdeskundige instellen
    services.AddIdentityCore<Ervaringsdeskundige>()
      .AddRoles<IdentityRole<Guid>>()
      .AddTokenProvider<DataProtectorTokenProvider<Ervaringsdeskundige>>("API")
      .AddEntityFrameworkStores<AccessibilityDbContext>()
      .AddDefaultTokenProviders();

    //Usermanager voor Bedrijf instellen
    services.AddIdentityCore<Bedrijf>()
      .AddRoles<IdentityRole<Guid>>()
      .AddTokenProvider<DataProtectorTokenProvider<Bedrijf>>("API")
      .AddEntityFrameworkStores<AccessibilityDbContext>()
      .AddDefaultTokenProviders();

    //Usermanager voor Medewerker instellen
    services.AddIdentityCore<Medewerker>()
      .AddRoles<IdentityRole<Guid>>()
      .AddTokenProvider<DataProtectorTokenProvider<Medewerker>>("API")
      .AddEntityFrameworkStores<AccessibilityDbContext>()
      .AddDefaultTokenProviders();

    services.Configure<IdentityOptions>(options => {
      options.Password.RequireDigit = false;
      options.Password.RequireLowercase = false;
      options.Password.RequireUppercase = false;
      options.Password.RequireNonAlphanumeric = false;
      options.Password.RequiredLength = 6;
      options.Password.RequiredUniqueChars = 1;
    });

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
        options.TokenValidationParameters = new TokenValidationParameters {
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

  private static void AddRepositories(IServiceCollection services) {
    services.AddScoped<IGebruikerRepository, SQLGebruikerRepository>();
    //services.AddScoped<ITrackingRepository, TrackingRepository>();
  }

  private static void AddServices(IServiceCollection services) {
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ITokenService, TokenService>();
  }


  private static void SetupMiddleware(WebApplication app) {
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment()) {
      app.UseSwagger();
      app.UseSwaggerUI();
    }

    // configure HTTPS
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
  }

}