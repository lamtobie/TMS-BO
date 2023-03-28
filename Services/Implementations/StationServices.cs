using AutoMapper;
using Databases.Entities;
using Databases.Interfaces;
using Repositories.StationRepository;
using Services.Helper.Exceptions.Station;
using Services.Interfaces;
using Services.Models.Pagination;
using Services.Models.Station;

namespace Services.Implementations;

public class StationServices : IStationServices
{
    private readonly ICommonServices _commonServices;
    private readonly IStationRepositories _stationRepositories;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public StationServices(IStationRepositories stationRepositories, ICommonServices commonServices, IMapper mapper)
    {
        _stationRepositories = stationRepositories;
        _commonServices = commonServices;
        _unitOfWork = _stationRepositories.UnitOfWork;
        _mapper = mapper;
    }

    public async Task<StationDto> CreateStation(StationDto stationCreationDto)
    {
        var existedStation = _stationRepositories.GetStationByCode(stationCreationDto.Code);
        if (existedStation != null)
        {
            throw new StationDuplicatedCodeException();
        }
        var station = _mapper.Map<StationDto, Station>(stationCreationDto);
        _stationRepositories.CreateStation(station);

        var stationDto = _mapper.Map<Station, StationDto>(station);
        return stationDto;
    }

    public Station DeleteStation(string code)
    {
        var existedStation = _stationRepositories.GetStationByCode(code);
        if (existedStation == null)
        {
            throw new StationNotFoundException();
        }
        return _stationRepositories.DeleteStation(code);
    }

    public async Task<PaginatedResultDto<Station>> GetAllStation(StationQuery query)
    {
        var queryable = _stationRepositories.GetAllStation(query);
        var result = _commonServices.CreatePaginationResponse<Station>(queryable, query);
        return result;
    }

    public async Task<StationDto> GetStationsByCode(string code)
    {
        var result = _stationRepositories.GetStationByCode(code);
        if (result == null)
        {
            throw new StationNotFoundException();
        }
        var resultDto = _mapper.Map<Station, StationDto>(result);
        return resultDto;
    }

    public async Task<StationDto> UpdateStation(StationDto stationDto, string stationCode)
    {
        await _unitOfWork.BeginTransactionAsync();
        var existedStation = _stationRepositories.GetStationByCode(stationCode);
        if (existedStation == null)
        {
            throw new StationNotFoundException();
        }
        _mapper.Map<StationDto, Station>(stationDto, existedStation);
        _stationRepositories.UpdateStation(existedStation);
        await _unitOfWork.SaveChangesAsync();
        var resultDto = _mapper.Map<Station, StationDto>(existedStation);
        await _unitOfWork.CommitTransactionAsync();
        return resultDto;
    }
}
