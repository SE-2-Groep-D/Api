using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace AccessibilityPanelAPI; 
public class Program { 
    public static void Main(string[] args) {
            // configure application
            var builder = WebApplication.CreateBuilder(args);
            SetupServices(builder.Services);
            
            // add middelware
            var app = builder.Build();
            SetupMiddleware(app);
            
            // run application
            app.Run();
    }

    private static void SetupServices(IServiceCollection services) { 
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<AccessibilityDbContext>(options =>
        options.UseSqlServer("hierconnectiestringzetten"));

        services.
    }
    private static void SetupMiddleware(WebApplication app) {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapControllers();
    }
}