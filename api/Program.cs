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
using Api.Repositories;
using Api.Repositories.AntwoordRepository;
using Api.Repositories.IGebruikerRepository;
using Microsoft.AspNetCore.Authentication.Google;
using Api.Repositories.VragenlijstRepository;
using Api.Repositories.VragenRepository;
using Api.Repositories.ITrackingRepository;
using Api.CustomMiddleware;

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

    if (builder.Environment.IsDevelopment()) {
      services.AddCors(options => {
        options.AddPolicy("AllowSpecific",
          builder => builder.WithOrigins("https://localhost:3000") // Replace with your React app's URL
                            .AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowCredentials()
                            .WithExposedHeaders("Set-Cookie"));
      });


    }
   
    
    ConnectToDatabase(services, builder);

    AddRepositories(services);
    AddServices(services);

    services.AddAutoMapper(typeof(AutoMapperProfiles));

    //Usermanager voor Gebruiker instellen
    services.AddIdentityCore<Gebruiker>(options => {
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 5;
      })
      .AddRoles<IdentityRole<Guid>>()
      .AddTokenProvider<DataProtectorTokenProvider<Gebruiker>>("API")
      .AddEntityFrameworkStores<AccessibilityDbContext>()
      .AddDefaultTokenProviders();

    //Usermanager voor Ervaringsdeskundige instellen
    services.AddIdentityCore<Ervaringsdeskundige>(options => {
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 5;
      })
      .AddRoles<IdentityRole<Guid>>()
      .AddTokenProvider<DataProtectorTokenProvider<Ervaringsdeskundige>>("API")
      .AddEntityFrameworkStores<AccessibilityDbContext>()
      .AddDefaultTokenProviders();

    //Usermanager voor Bedrijf instellen
    services.AddIdentityCore<Bedrijf>(options => {
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 5;
      })
      .AddRoles<IdentityRole<Guid>>()
      .AddTokenProvider<DataProtectorTokenProvider<Bedrijf>>("API")
      .AddEntityFrameworkStores<AccessibilityDbContext>()
      .AddDefaultTokenProviders();

    //Usermanager voor Medewerker instellen
    services.AddIdentityCore<Medewerker>(options => {
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = true;
        options.Password.RequiredLength = 5;
      })
      .AddRoles<IdentityRole<Guid>>()
      .AddTokenProvider<DataProtectorTokenProvider<Medewerker>>("API")
      .AddEntityFrameworkStores<AccessibilityDbContext>()
      .AddDefaultTokenProviders();

    AddAuthentication(services, builder);

  }

  private static void AddRepositories(IServiceCollection services) {
    services.AddScoped<IGebruikerRepository, SQLGebruikerRepository>();
    services.AddScoped<IVragenlijstRepository, SQLVragenlijstRepository>();
    services.AddScoped<IVraagRepository, SQLVraagRepository>();
    services.AddScoped<IAntwoordRepository, SQLAntwoordRepository>();
    services.AddScoped<ITrackingRepository, TrackingRepository>();
    services.AddScoped<IOnderzoekRepository, SQLOnderzoekRepository>(); 

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
      //app.UseCors(builder => {
      //  builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
      //  Console.WriteLine("Setup cors");
      //});

    }

    app.UseStaticFiles();

    // configure HTTPS
    app.UseHttpsRedirection();

    app.UseCors("AllowSpecific");


    app.UseMiddleware<AuthorizationHeaderMiddleware>();
    app.UseAuthentication();
    
    app.UseAuthorization();
    
    app.MapControllers();
    
  }


  private static void ConnectToDatabase(IServiceCollection services, WebApplicationBuilder builder) {
    var connectionString = builder.Configuration.GetConnectionString("APIDbConnectionString");
    var dbType = builder.Configuration["DatabaseType"];

    Console.WriteLine(connectionString);
    
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
  }

  private static void AddAuthentication(IServiceCollection services, WebApplicationBuilder builder) {
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

}