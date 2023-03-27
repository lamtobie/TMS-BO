using Databases.Entities;
using Services.Models.VehicleType;

namespace Repositories.VehicleTypeRepository;

public interface IVehicleTypeRepositories
{
    IQueryable<VehicleType> GetAllVehicleType(VehicleTypeQuery queryData);
    void CreateVehicleType(VehicleType vehicleType);
    VehicleType? GetVehicleTypeByCode(string code);
    VehicleType? GetVehicleTypeByName(string name);
    VehicleType DeleteVehicleType(string code);
    VehicleType UpdateVehicleType(VehicleType vehicleType);
    VehicleType? GetVehicleTypeBaseInformation(string code);
}
