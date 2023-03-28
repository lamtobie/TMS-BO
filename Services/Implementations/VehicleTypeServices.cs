using AutoMapper;
using Databases.Entities;
using Repositories.VehicleTypeRepository;
using Services.Helper.Exceptions.VehicleType;
using Services.Interfaces;
using Services.Models.Pagination;
using Services.Models.VehicleType;

namespace Services.Implementations;

public class VehicleTypeServices : IVehicalTypeServices
{
    private readonly ICommonServices _commonServices;
    private readonly IVehicleTypeRepositories _vehicleTypeRepositories;
    private readonly IMapper _mapper;

    public VehicleTypeServices(IVehicleTypeRepositories vehicleTypeRepositories, ICommonServices commonServices, IMapper mapper)
    {
        _vehicleTypeRepositories = vehicleTypeRepositories;
        _commonServices = commonServices;
        _mapper = mapper;
    }

    public VehicleType CreateVehicleType(VehicleTypeDto vehicleTypeCreationDto)
    {
        var vehicleType = _mapper.Map<VehicleTypeDto, VehicleType>(vehicleTypeCreationDto);
        if (vehicleType.Code == null)
        {
            vehicleType.Code = "VT" + System.Guid.NewGuid().ToString().Substring(0, 8);
        }
        var checkVehicleType = _vehicleTypeRepositories.GetVehicleTypeByCode(vehicleType.Code);

        if (checkVehicleType != null)
        {
            throw new VehicleTypeDuplicatedCodeException();
        }

        checkVehicleType = _vehicleTypeRepositories.GetVehicleTypeByName(vehicleType.Name);

        if (checkVehicleType != null)
        {
            throw new VehicleTypeDuplicatedNameException();
        }

        _vehicleTypeRepositories.CreateVehicleType(vehicleType);
        return vehicleType;
    }

    public VehicleTypeDto DeleteVehicleType(string code)
    {
        var existedVehicleType = _vehicleTypeRepositories.GetVehicleTypeByCode(code);
        if (existedVehicleType == null)
        {
            throw new VehicleTypeNotFound();
        }

        if (existedVehicleType.Vehicles.Count != 0)
        {
            throw new VehicleTypeExistVehicle();
        }

        _vehicleTypeRepositories.DeleteVehicleType(code);

        var getDto = _vehicleTypeRepositories.GetVehicleTypeByCode(existedVehicleType.Code);
        var returnDto = _mapper.Map<VehicleType, VehicleTypeDto>(getDto);

        return returnDto;
    }

    public async Task<PaginatedResultDto<VehicleTypeDto>> GetAllVehicleType(VehicleTypeQuery query)
    {
        var queryable = _vehicleTypeRepositories.GetAllVehicleType(query);
        var result = _commonServices.CreatePaginationDtoResponse<VehicleType, VehicleTypeDto>(queryable, query);
        return result;
    }

    public async Task<VehicleTypeDto> GetVehicleTypeByCode(string code)
    {
        var result = _vehicleTypeRepositories.GetVehicleTypeByCode(code);
        if (result == null)
        {
            throw new VehicleTypeNotFound();
        }

        var resultDto = _mapper.Map<VehicleType, VehicleTypeDto>(result);
        return resultDto;
    }

    public VehicleTypeDto UpdateVehicleType(string vehicleTypeCode, VehicleTypeDto vehicleTypeBaseDto)
    {
        var existedVehicleType = _vehicleTypeRepositories.GetVehicleTypeBaseInformation(vehicleTypeCode);
        if (existedVehicleType == null)
        {
            throw new VehicleTypeNotFound();
        }

        existedVehicleType.Height = vehicleTypeBaseDto.Height ?? existedVehicleType.Height;
        existedVehicleType.Name = vehicleTypeBaseDto.Name ?? existedVehicleType.Name;
        existedVehicleType.Length = vehicleTypeBaseDto.Length ?? existedVehicleType.Length;
        existedVehicleType.Width = vehicleTypeBaseDto.Width ?? existedVehicleType.Width;
        existedVehicleType.MaximumCapacity = vehicleTypeBaseDto.MaximumCapacity ?? existedVehicleType.MaximumCapacity;
        existedVehicleType.MaximumPayload = vehicleTypeBaseDto.MaximumPayload ?? existedVehicleType.MaximumPayload;
        existedVehicleType.Status = vehicleTypeBaseDto.Status ?? existedVehicleType.Status;
     
        _vehicleTypeRepositories.UpdateVehicleType(existedVehicleType);

        var getDto = _vehicleTypeRepositories.GetVehicleTypeByCode(existedVehicleType.Code);
        var returnDto = _mapper.Map<VehicleType, VehicleTypeDto>(getDto);
        return returnDto;
    }
}
