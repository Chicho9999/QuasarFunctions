using FuegoDeQuasar.Service;
using FuegoDeQuasar.Service.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using FuegoDeQuasarFunctions;

[assembly: FunctionsStartup(typeof(Startup))]
namespace FuegoDeQuasarFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            builder.Services.AddSingleton<ISateliteService>(s => new SateliteService());
        }
    }
}