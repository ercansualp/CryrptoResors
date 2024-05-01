using CryrptoResorAPI.Services;

namespace CryrptoResorAPI
{
    public static class ServiceRegistration
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddSingleton<ICoinService, CoinService>();
        }
    }
}
