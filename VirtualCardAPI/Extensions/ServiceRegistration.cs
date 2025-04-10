using VirtualCardAPI.Services.Abstract;
using VirtualCardAPI.Services.Concrete;

namespace VirtualCardAPI.Extensions
{
    public static class ServiceRegistration
    {
        public static void ConfigureFakeServices(this IServiceCollection services)
        {
            services.AddScoped<ICardValidationService, CardValidationService>();
            services.AddScoped<IPaymentGatewayService, PaymentGatewayService>();
            services.AddScoped<IAuthService, FakeAuthService>();

        }
    }
}
