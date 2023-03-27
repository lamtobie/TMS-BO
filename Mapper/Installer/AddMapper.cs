using Mapper.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Mapper.Installer;

public static class AddMapperConfiguration
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DeliveryOrderProfile));
        services.AddAutoMapper(typeof(DeliveryPackageProfile));
        services.AddAutoMapper(typeof(DeliveryPackageGroupProfile));
        services.AddAutoMapper(typeof(DeliveryRouteProfile));
        services.AddAutoMapper(typeof(DeliveryRouteSegmentProfile));
        services.AddAutoMapper(typeof(StationProfile));
        services.AddAutoMapper(typeof(EmployeeProfile));
        services.AddAutoMapper(typeof(DeliveryOrderLineProfile));
        services.AddAutoMapper(typeof(DataAttributeProfile));
        services.AddAutoMapper(typeof(DeliverySessionProfile));
        services.AddAutoMapper(typeof(DeliverySessionLineProfile));
        services.AddAutoMapper(typeof(DeliverySessionGroupProfile));
        services.AddAutoMapper(typeof(VehicleProfile));
        services.AddAutoMapper(typeof(VehicleTypeProfile));
        services.AddAutoMapper(typeof(DeliveryOrderGroupProfile));
        return services;
    }
}