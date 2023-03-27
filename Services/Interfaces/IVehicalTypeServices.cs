using Databases.Entities;
using Services.Models.Pagination;
using Services.Models.Vehicle;
using Services.Models.VehicleType;

namespace Services.Interfaces
{
    public interface IVehicalTypeServices
    {
        VehicleType CreateVehicleType(VehicleTypeDto vehicleTypeBaseCreationDto);
        Task<PaginatedResultDto<VehicleTypeDto>> GetAllVehicleType(VehicleTypeQuery query);
        Task<VehicleTypeDto> GetVehicleTypeByCode(string code);
        VehicleTypeDto DeleteVehicleType(string code);
        VehicleTypeDto UpdateVehicleType(string vehicleTypeCode, VehicleTypeDto vehicleTypeBaseDto);
    }
}
