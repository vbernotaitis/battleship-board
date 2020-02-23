using BattleShip.Shooter.Random;
using BattleShipBoard.Engine;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BattleShipBoard.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(x =>
                new BattleShipGame(
                    new BattleShipShooterExceptionWrapper(typeof(BattleShipShooter)),
                    new BattleShipShooterExceptionWrapper(typeof(BattleShipShooter), "Petras"))
            );
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
