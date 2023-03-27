using Databases.Entities;
using Services.Models.Pagination;
using Services.Models.Vehicle;

namespace Services.Interfaces;

public interface IVehicleServices
{
    public Task<PaginatedResultDto<VehicleDto>> GetAll(VehicleQuery query);
    public Task<VehicleDto> GetOneByCode(string code);
    public Task<VehicleDto> Create(VehicleToCreateDto vehicleDto);
    public Task<VehicleDto> Update(VehicleToUpdateDto vehicleDto, string code);
    public Task<VehicleDto> Delete(string code);
}