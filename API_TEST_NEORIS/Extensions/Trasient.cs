using TEST.CORE.Interface.Accounts;
using TEST.CORE.Interface.Clients;
using TEST.CORE.Interface.Movement;
using TEST.INFRAESTRUCTURE.Repositories.Accounts;
using TEST.INFRAESTRUCTURE.Repositories.Clients;
using TEST.INFRAESTRUCTURE.Repositories.Movements;

namespace API_TEST_NEORIS.Extensions
{
    public static class Trasient
    {
        public static void configureTrasient(this IServiceCollection services)
        {
            services.AddScoped<IPerson, ClientRepository>();

            services.AddScoped<IAccount, AccountRepository>();

            services.AddScoped<IMovement, MovementRepository>();

        }
    }
}
