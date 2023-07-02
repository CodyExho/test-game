using Game.DataAccess;
using Game.Operations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Game.CompositionRoot
{
    public class Bootstrap
    {
        public static void Configure(IServiceCollection container, IConfiguration configuration)
        {
            container.AddScoped(factory => new MongoClient(configuration["DatabaseSettings:ConnectionString"]));

            container.AddScoped(factory =>
            {
                var client = (MongoClient)factory.GetService(typeof(MongoClient));
                return client.GetDatabase(configuration["DatabaseSettings:DatabaseName"]);
            });

            container.AddTransient<IUnitOfWork, UnitOfWork>();

            container.AddAutoMapper(
                typeof(ReceiverProfile)
                );

            container.AddTransient<IEventOperations, EventOperations>();
            container.AddTransient<IOfferOperations, OfferOperations>();
        }
    }
}