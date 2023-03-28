using Repositories;
using Services.Implementations;
using Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Eton.TMS.OpBackOffice.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            // Repository register
            services.AddRepositories();

            // Services register
            services.AddTransient<ICommonServices, CommonServices>();
            services.AddTransient<IAuthorizedServices, AuthorizedServices>();
            services.AddTransient<IDeliveryOrderServices, DeliveryOrderServices>();
            services.AddTransient<IDeliveryOrderLineServices, DeliveryOrderLineServices>();
            services.AddTransient<IDeliveryPackageServices, DeliveryPackageServices>();
            services.AddTransient<IDeliveryRouteServices, DeliveryRouteServices>();
            services.AddTransient<IEmployeeServices, EmployeeServices>();
            services.AddTransient<IStationServices, StationServices>();
            services.AddTransient<IDataAttributeServices, DataAttributeServices>();
            services.AddTransient<IVehicleServices, VehicleServices>();
            services.AddTransient<IVehicalTypeServices, VehicleTypeServices>();
            services.AddTransient<IDeliverySessionServices, DeliverySessionServices>();
            services.AddTransient<IDeliverySessionLineServices, DeliverySessionLineServices>();
            services.AddTransient<IDeliveringServices, DeliveringServices>();
            services.AddTransient<IDeliveryOrderGroupServices, DeliveryOrderGroupServices>();
            services.AddTransient<IAddressServices, AddressServices>();      

            return services;
        }
    }
}
