using ArenaGame.Models;
using ArenaGame.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ArenaGame
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            _ = builder.Services.AddControllers();
            _ = builder.Services.AddSingleton<IArenaGameRepository, ArenaGameRepository>();
            _ = builder.Services.AddScoped<IArenaService, ArenaService>();
            _ = builder.Services.AddScoped<IBattleService, BattleService>();

            WebApplication app = builder.Build();

            _ = app.UseAuthorization();
            _ = app.MapControllers();
            app.Run();
        }
    }
}
