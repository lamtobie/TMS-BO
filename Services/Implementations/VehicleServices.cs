using AutoMapper;
using Databases.Entities;
using Repositories.VehicleRepository;
using Services.Helper.Exceptions.Vehicle;
using Services.Interfaces;
using Services.Models.Pagination;
using Services.Models.Vehicle;
using Microsoft.AspNetCore.Mvc;
using Databases.Interfaces;

namespace Services.Implementations;

public class VehicleServices : IVehicleServices
{
    private readonly IVehicleRepositories _vehicleRepositories;
    private readonly ICommonServices _commonServices;

    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;

    public VehicleServices(
        IVehicleRepositories vehicleRepositories, 
        ICommonServices commonServices, 
        IMapper mapper
        )
    {
        _vehicleRepositories = vehicleRepositories;
        _commonServices = commonServices;
        _mapper = mapper;
        _unitOfWork = _vehicleRepositories.UnitOfWork;
    }

    public async Task<PaginatedResultDto<VehicleDto>> GetAll(VehicleQuery query)
    {
        var queryResult = _vehicleRepositories
            .GetAllVehicles(query);
       var result = _commonServices.CreatePaginationDtoResponse<Vehicle, VehicleDto>(queryResult, query);
        return result;
    }

    public async Task<VehicleDto> GetOneByCode(string code)
    {
        var result = _vehicleRepositories.GetVehicleByCode(code);
        if (result == null)
        {
            throw new VehicleNotFoundException();
        }
        var resultDto = _mapper.Map<Vehicle, VehicleDto>(result);
        return resultDto;
    }

    public async Task<VehicleDto> Create(VehicleToCreateDto vehicleDto)
    {
        var vehicle = _mapper.Map<VehicleToCreateDto, Vehicle>(vehicleDto);
        if (vehicle.Code == null)
        {
            vehicle.Code = "V" + System.Guid.NewGuid().ToString().Substring(0, 8);
        }
        
        var checkVehicle = _vehicleRepositories.GetVehicleByCode(vehicle.Code);
        if(checkVehicle != null)
        {
            throw new VehicleDuplicatedCodeException();
        }

        _vehicleRepositories.CreateVehicle(vehicle);
        await _unitOfWork.SaveChangesAsync();

        var getVehicle = _vehicleRepositories.GetVehicleByCode(vehicle.Code);
        var resultDto = _mapper.Map<Vehicle, VehicleDto>(getVehicle);
        return resultDto;
    }

    public async Task<VehicleDto> Update(VehicleToUpdateDto vehicleDto, string code)
    {
        var existedVehicle = _vehicleRepositories.GetBaseVehicleInformation(code);
        if (existedVehicle == null)
        {
            throw new VehicleNotFoundException();
        }

        existedVehicle.Status = vehicleDto.Status ?? existedVehicle.Status;
        existedVehicle.NumberPlate = vehicleDto.NumberPlate ?? existedVehicle.NumberPlate;
        existedVehicle.VehicleTypeCode = vehicleDto.VehicleTypeCode ?? existedVehicle.VehicleTypeCode;
        
        _vehicleRepositories.UpdateVehicle(existedVehicle);
        await _unitOfWork.SaveChangesAsync();

        var getVehicle = _vehicleRepositories.GetVehicleByCode(existedVehicle.Code);
        var resultDto = _mapper.Map<Vehicle, VehicleDto>(getVehicle);
        return resultDto;
    }

    public async Task<VehicleDto> Delete(string code)
    {
        var vehicle = _vehicleRepositories.GetBaseVehicleInformation(code);
        if (vehicle == null)
        {
            throw new VehicleNotFoundException();
        }
        var getVehicle = _vehicleRepositories.GetVehicleByCode(vehicle.Code);

        _vehicleRepositories.DeleteVehicle(vehicle);
        await _unitOfWork.SaveChangesAsync();

        var resultDto = _mapper.Map<Vehicle, VehicleDto>(getVehicle);
        return resultDto;

    }
}