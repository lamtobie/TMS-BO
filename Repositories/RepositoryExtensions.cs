using Repositories.AddressRepository;
using Repositories.DataAttributeRepository;
using Repositories.DeliveryOrderGroupRepository;
using Repositories.DeliveryOrderLineRepository;
using Repositories.DeliveryOrderRepository;
using Repositories.DeliveryPackageGroupRepository;
using Repositories.DeliveryPackageRepository;
using Repositories.DeliveryRouteRepository;
using Repositories.DeliveryRouteSegmentRepository;
using Repositories.DeliverySessionGroupRepository;
using Repositories.DeliverySessionLineRepository;
using Repositories.DeliverySessionRepository;
using Repositories.EmployeeRepository;
using Repositories.StationRepository;
using Repositories.VehicleRepository;
using Repositories.VehicleTypeRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Repositories
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IDeliveryOrderRepositories), typeof(DeliveryOrderRepositories));
            services.AddScoped(typeof(IDeliveryPackageRepositories), typeof(DeliveryPackageRepositories));
            services.AddScoped(typeof(IDeliveryPackageGroupRepositories), typeof(DeliveryPackageGroupRepositories));
            services.AddScoped(typeof(IDeliveryRouteRepositories), typeof(DeliveryRouteRepositories));
            services.AddScoped(typeof(IDeliveryRouteSegmentRepositories), typeof(DeliveryRouteSegmentRepositories));
            services.AddScoped(typeof(IStationRepositories), typeof(StationRepositories));
            services.AddScoped(typeof(IEmployeeRepositories), typeof(EmployeeRepositories));
            services.AddScoped(typeof(IDeliveryOrderLineRepositories), typeof(DeliveryOrderLineRepositories));
            services.AddScoped(typeof(IDataAttributeRepositories), typeof(DataAttributeRepositories));
            services.AddScoped(typeof(IDeliverySessionRepositories), typeof(DeliverySessionRepositories));
            services.AddScoped(typeof(IDeliverySessionLineRepositories), typeof(DeliverySessionLineRepositories));
            services.AddScoped(typeof(IDeliverySessionGroupRepositories), typeof(DeliverySessionGroupRepositories));
            services.AddScoped(typeof(IVehicleRepositories), typeof(VehicleRepositories));
            services.AddScoped(typeof(IVehicleTypeRepositories), typeof(VehicleTypeRepositories));
            services.AddScoped(typeof(IDeliveryOrderGroupRepositories), typeof(DeliveryOrderGroupRepositories));
            services.AddScoped(typeof(IAddressRepositories), typeof(AddressRepositories));
            return services;
        }
    }
}
