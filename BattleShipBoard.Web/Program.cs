using BattleShip.Shooter.Random;
using BattleShipBoard.Engine;
using BattleShipBoard.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddTransient(x =>
    new BattleShipGame(
        new BattleShipShooterTracker(typeof(BattleShipShooter)),
        new BattleShipShooterTracker(typeof(BattleShipShooter), "Petras"))
);

await builder.Build().RunAsync();
