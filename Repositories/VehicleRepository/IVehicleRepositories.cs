using Repositories;
using Databases.Entities;
using Services.Models.Vehicle;

namespace Repositories.VehicleRepository;

public interface IVehicleRepositories : IRepository<Vehicle, string>
{
    public IQueryable<Vehicle> GetAllVehicles(VehicleQuery queryData);
    public Vehicle? GetVehicleByCode(string code);
    public Vehicle? GetBaseVehicleInformation(string code);
    public Vehicle CreateVehicle(Vehicle vehicle);
    public Vehicle UpdateVehicle(Vehicle vehicle);
    public Vehicle? DeleteVehicle(Vehicle vehicle);
}