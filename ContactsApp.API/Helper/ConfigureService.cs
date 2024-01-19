using ContactsApp.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactsApp.API
{
    public class ConfigureService
    {
        /// <summary>
        /// This is used to add the dependencies 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void Configure(IServiceCollection services,IConfiguration configuration)
        {
            services.AddScoped<IContactsService, ContactsService>();

        }
    }
}
