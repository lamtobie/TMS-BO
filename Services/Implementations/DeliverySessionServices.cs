using AutoMapper;
using Databases.Entities;
using Repositories.DeliverySessionLineRepository;
using Repositories.DeliverySessionRepository;
using Repositories.DeliveryPackageGroupRepository;
using Repositories.DeliveryPackageRepository;
using Services.Interfaces;
using Services.Models.DeliverySession;
using Services.Models.Pagination;
using Repositories.EmployeeRepository;
using Services.Helper.Enums;
using Services.Helper.Exceptions.DeliverySession;
using Repositories.VehicleRepository;
using Repositories.StationRepository;
using Services.Models.DeliverySessionLine;
using Repositories.DeliveryOrderRepository;
using Services.Models.Vehicle;
using Services.Models.Employee;
using Services.Models.Station;
using Services.Models.VehicleType;
using Repositories.DeliverySessionGroupRepository;
using Services.Helper.Exceptions.Employee;
using Services.Helper.Exceptions.Vehicle;
using Microsoft.EntityFrameworkCore;
using Services.Models.DeliveryOrder;
using Databases.Interfaces;
using Services.Helper.Extensions;
using Services.Helper.Exceptions.Station;

namespace Services.Implementations;

public class DeliverySessionServices : IDeliverySessionServices
{
    private readonly ICommonServices _commonServices;

    private readonly IDeliveryOrderServices _deliveryOrderServices;
    private readonly IDeliverySessionLineServices _deliverySessionLineServices;

    private readonly IDeliverySessionRepositories _deliverySessionRepositories;
    private readonly IDeliveryPackageRepositories _deliveryPackageRepositories;
    private readonly IDeliveryPackageGroupRepositories _deliveryPackageGroupRepositories;
    private readonly IDeliverySessionLineRepositories _deliverySessionLineRepositories;
    private readonly IDeliverySessionGroupRepositories _deliverySessionGroupRepositories;
    private readonly IEmployeeRepositories _employeeRepositories;
    private readonly IVehicleRepositories _vehicleRepositories;
    private readonly IStationRepositories _stationRepositories;
    private readonly IDeliveryOrderRepositories _deliveryOrderRepositories;
    private readonly IAuthorizedServices _authorizedServies;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeliverySessionServices(
        ICommonServices commonServices,
        IDeliveryOrderServices deliveryOrderServices,
        IDeliverySessionLineServices deliverySessionLineServices,
        IDeliverySessionRepositories deliverySessionRepositories,
        IDeliveryPackageRepositories deliveryPackageRepositories,
        IDeliveryPackageGroupRepositories deliveryPackageGroupRepositories,
        IDeliverySessionLineRepositories deliverySessionLineRepositories,
        IDeliverySessionGroupRepositories deliverySessionGroupRepositories,
        IEmployeeRepositories employeeRepositories,
        IVehicleRepositories vehicleRepositories,
        IStationRepositories stationRepositories,
        IDeliveryOrderRepositories deliveryOrderRepositories,
        IAuthorizedServices authorizedServies,
        IMapper mapper
        )
    {
        _commonServices = commonServices;
        _deliveryOrderServices = deliveryOrderServices;
        _deliverySessionLineServices = deliverySessionLineServices;
        _deliverySessionRepositories = deliverySessionRepositories;
        _unitOfWork = deliverySessionRepositories.UnitOfWork;
        _deliveryPackageRepositories = deliveryPackageRepositories;
        _deliveryPackageGroupRepositories = deliveryPackageGroupRepositories;
        _deliverySessionLineRepositories = deliverySessionLineRepositories;
        _deliverySessionGroupRepositories = deliverySessionGroupRepositories;
        _employeeRepositories = employeeRepositories;
        _vehicleRepositories = vehicleRepositories;
        _stationRepositories = stationRepositories;
        _deliveryOrderRepositories = deliveryOrderRepositories;
        _authorizedServies = authorizedServies;
        _mapper = mapper;
    }

    public async Task<PaginatedResultDto<DeliverySessionDto>> GetAll(DeliverySessionQuery query)
    {
        var queryable = _deliverySessionRepositories.GetAllDeliverySessions(query);
        var result = _commonServices.CreatePaginationDtoResponse<DeliverySession, DeliverySessionDto>(queryable, query);
        return result;
    }

    public async Task<PaginatedResultDto<DeliverySessionTreeResultDto>> GetTree(DeliverySessionQuery query)
    {
        var queryable = _deliverySessionRepositories.GetAllDeliverySessions(query);
        queryable = queryable.Where(x => x.ParentCode == null);
        var queryableResult = queryable
                .Select(x => new DeliverySessionTreeResultDto()
                {
                    Code = x.Code,
                    SessionType = x.SessionType,
                    ParentCode = x.ParentCode,
                    DriverCode = x.DriverCode,
                    CoordinatorCode = x.CoordinatorCode,
                    VehicleCode = x.VehicleCode,
                    StartStationCode = x.StartStationCode,
                    EndStationCode = x.EndStationCode,
                    Status = x.Status,
                    Note = x.Note,
                    Excepted = x.Excepted,
                    ReasonCancel = x.ReasonCancel,
                    ReasonReject = x.ReasonReject,
                    TotalReceivedItems = x.TotalReceivedItems,
                    Vehicle = new VehicleDto()
                    {
                        Code = x.Vehicle.Code,
                        NumberPlate = x.Vehicle.NumberPlate,
                        VehicleTypeInformation = new VehicleTypeDto()
                        {
                            Code = x.Vehicle.VehicleType.Code,
                            Name = x.Vehicle.VehicleType.Name
                        }
                    },
                    Children = x.Childrens.Count > 0
                        ? x.Childrens.Select(c => new DeliverySessionDto()
                        {
                            Code = c.Code,
                            SessionType = c.SessionType,
                            ParentCode = c.ParentCode,
                            DriverCode = c.DriverCode,
                            CoordinatorCode = c.CoordinatorCode,
                            VehicleCode = c.VehicleCode,
                            StartStationCode = c.StartStationCode,
                            EndStationCode = c.EndStationCode,
                            Status = c.Status,
                            Note = c.Note,
                            Excepted = c.Excepted,
                            ReasonCancel = c.ReasonCancel,
                            ReasonReject = c.ReasonReject,
                            TotalReceivedItems = c.TotalReceivedItems,
                            Vehicle = new VehicleDto()
                            {
                                Code = c.Vehicle.Code,
                                NumberPlate = c.Vehicle.NumberPlate,
                                VehicleTypeInformation = new VehicleTypeDto()
                                {
                                    Code = c.Vehicle.VehicleType.Code,
                                    Name = c.Vehicle.VehicleType.Name
                                }
                            },
                            Driver = _mapper.Map<Employee, EmployeeDto>(c.Driver),
                            Coordinator = _mapper.Map<Employee, EmployeeDto>(c.Coordinator),
                            StartStation = _mapper.Map<Station, StationDto>(c.StartStation),
                            EndStation = _mapper.Map<Station, StationDto>(c.EndStation),
                            DeliverySessionLines = _mapper.Map<List<DeliverySessionLine>, List<DeliverySessionLineDto>>(c.DeliverySessionLines.ToList()),
                            CreatedAt = c.CreatedAt,
                            CreatedBy = c.CreatedBy,
                            UpdatedAt = c.UpdatedAt,
                            UpdatedBy = c.UpdatedBy
                        }).ToList()
                        : null,
                    Driver = _mapper.Map<Employee, EmployeeDto>(x.Driver),
                    Coordinator = _mapper.Map<Employee, EmployeeDto>(x.Coordinator),
                    StartStation = _mapper.Map<Station, StationDto>(x.StartStation),
                    EndStation = _mapper.Map<Station, StationDto>(x.EndStation),
                    DeliverySessionLines = _mapper.Map<List<DeliverySessionLine>, List<DeliverySessionLineDto>>(x.DeliverySessionLines.ToList()),
                    CreatedAt = x.CreatedAt,
                    CreatedBy = x.CreatedBy,
                    UpdatedAt = x.UpdatedAt,
                    UpdatedBy = x.UpdatedBy
                })
                .AsQueryable();
        var result = _commonServices.CreatePaginationResponse<DeliverySessionTreeResultDto>(queryableResult, query);
        return result;
    }

    public string RandomSessionCode()
    {
        return "DS" + Guid.NewGuid().ToString("n").Substring(0, 8).ToUpper();
    }

    public async Task<DeliverySessionDto> GetOneByCode(string code)
    {
        var deliverySession = GetDeliverySessionByCode(code);

        var result = _mapper.Map<DeliverySession, DeliverySessionDto>(deliverySession);

        return result;
    }

    public async Task<DeliverySessionDto> Create(DeliverySessionToCreateDto data)
    {
        await _unitOfWork.BeginTransactionAsync();

        if (data.Data.Count == 0)
        {
            throw new Exception("Dữ liệu phiên không hợp lệ");
        }

        ValidateSessions(data.Data);

        var sessionCode = string.Empty;
        var sessionGroupCode = string.Empty;

        if (data.Data.Count > 1)
        {
            sessionGroupCode = _deliverySessionGroupRepositories.Init().Code;
        }
        else
        {
            sessionGroupCode = null;
        }

        data.Data.ForEach(ds =>
        {
            var sessionDto = ds.CreateSession(null, sessionGroupCode);
            sessionDto.SessionType = SessionTypeEnum.Pickup.ToString();
            var session = _mapper.Map<DeliverySessionDto, DeliverySession>(sessionDto);
            _deliverySessionRepositories.Add(session);
            _deliveryOrderServices.UpdateDOBySession(sessionDto);

            sessionCode = sessionDto.Code;
        });

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitTransactionAsync();

        return await GetOneByCode(sessionCode);
    }

    public async Task<DeliverySessionDto> UpdateSessionLines(string deliverySessionCode, DeliverySessionDto deliverySessionDto)
    {
        await _unitOfWork.BeginTransactionAsync();

        ValidateSession(deliverySessionDto);

        var session = GetDeliverySessionByCode(deliverySessionCode);

        if (session.DeliverySessionLines == null)
        {
            // Add new session lines
            var sessionLinesToCreate = deliverySessionDto.DeliverySessionLines?.Where(x => x.Code == null).ToList();
            if (sessionLinesToCreate?.Count > 0)
            {
                _deliverySessionLineServices.CreateMany(sessionLinesToCreate, deliverySessionDto);
                // AssignSessionToDOs(deliverySessionCode, sessionLinesToCreate);
            }
        }
        else
        {
            // Delete all session lines
            if (deliverySessionDto.DeliverySessionLines?.Count == 0 && session.DeliverySessionLines.Count > 0)
            {
                _deliverySessionLineServices.DeleteAllSessionLines(session);
                // UnassignSessionToDOs(deliverySessionDto.DeliverySessionLines);
            }

            // Update session lines
            var sessionLinesToUpdate = session.DeliverySessionLines.Where(x => deliverySessionDto.DeliverySessionLines.Any(y => x.Code == y.Code)).ToList();
            if (sessionLinesToUpdate.Count > 0)
            {
                _deliverySessionLineServices.UpdateMany(sessionLinesToUpdate, deliverySessionDto);
            }

            // Delete session lines
            var sessionLinesToDelete = session.DeliverySessionLines.Where(x => !deliverySessionDto.DeliverySessionLines.Any(y => x.Code == y.Code)).ToList();
            sessionLinesToDelete.ForEach(x =>
            {
                _deliverySessionLineServices.DeleteOne(x);
            });
            // UnassignSessionToDOs(_mapper.Map<List<DeliverySessionLine>, List<DeliverySessionLineDto>>(sessionLinesToDelete));

            // Add new session lines
            var sessionLinesToCreate = deliverySessionDto.DeliverySessionLines.Where(x => string.IsNullOrEmpty(x.Code)).ToList();
            if (sessionLinesToCreate.Count > 0)
            {
                _deliverySessionLineServices.CreateMany(sessionLinesToCreate, deliverySessionDto);
                // AssignSessionToDOs(deliverySessionCode, sessionLinesToCreate);
            }
        }

        _deliveryOrderServices.UpdateDOBySession(deliverySessionDto);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitTransactionAsync();

        return await GetOneByCode(deliverySessionCode);
    }
    
    public async Task<DeliverySessionDto> HandedOver(string deliverySessionCode, DeliverySessionConfirmDto data)
    {
        await _unitOfWork.BeginTransactionAsync();

        var session = _deliverySessionRepositories.GetSessionForHandedOver(deliverySessionCode);

        if (session.Status != SessionStatusEnum.New.ToString())
        {
            throw new DeliverySessionInvalidStatusToSwitchException();
        }

        var userId = _authorizedServies.GetUserId();
        var sessionDto = _mapper.Map<DeliverySession, DeliverySessionDto>(session);
        sessionDto.HandedOver(userId, data);
        session = _mapper.Map<DeliverySessionDto, DeliverySession>(sessionDto);
        foreach (var sessionDeliveryOrder in session.DeliveryOrders)
        {
            sessionDeliveryOrder.Status = DeliveryOrderStatusEnum.Picking.ToString();
        }
        _deliverySessionRepositories.Update(session);
    
        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitTransactionAsync();

        return await GetOneByCode(deliverySessionCode);
    }

    public async Task<List<DeliverySession>> CreateDropOffSession(DeliverySession sessionPickup)
    {
        var dropOffSessions = new List<DeliverySession>();
        var dropOffCreated = sessionPickup.DeliverySessionLines
            .GroupBy(e => e.DeliveryOrderParentCode)
            .Select(g => new {doCode = g.Key, lines = g.ToList()});
        
        dropOffCreated.ToList().ForEach(e =>
        {
            var dropOffSession = new DeliverySession();
            dropOffSession.Code = RandomSessionCode();
            dropOffSession.DriverCode = sessionPickup.DriverCode;
            dropOffSession.VehicleCode = sessionPickup.VehicleCode;
            dropOffSession.SessionType = SessionTypeEnum.Dropoff.ToString();
            dropOffSession.ParentCode = sessionPickup.Code;
            dropOffSession.Status = SessionStatusEnum.New.ToString();

            var dropOffLines = e.lines.ShallowClone();
            
            dropOffLines.ForEach(line =>
            {
                line.DeliverySession = null;
                line.DeliverySessionCode = dropOffSession.Code;
                line.Id = new Guid();
            });
            
            var deliveryOrders = sessionPickup.DeliveryOrders.Where(x => x.Code == e.doCode || x.ParentCode == e.doCode).ToList();
            
            deliveryOrders.ForEach(x =>
            {
                x.Session = null;
                x.SessionCode = dropOffSession.Code;
            });

            dropOffSession.DeliverySessionLines = dropOffLines;
            
            dropOffSessions.Add(dropOffSession);
        });
        await _deliverySessionRepositories.AddRangeAsync(dropOffSessions);
        return dropOffSessions;
    }

    public async Task<DeliverySessionDto> Cancel(string deliverySessionCode, DeliverySessionConfirmDto data)
    {
        var session = GetDeliverySessionByCode(deliverySessionCode);

        if (session.Status != SessionStatusEnum.New.ToString() && session.Status != SessionStatusEnum.AConfirmed.ToString())
        {
            throw new DeliverySessionInvalidStatusToSwitchException();
        }

        await _unitOfWork.BeginTransactionAsync();

        var sessionDto = _mapper.Map<DeliverySession, DeliverySessionDto>(session);
        sessionDto.Cancel(data.ReasonCancel);
        session = _mapper.Map<DeliverySessionDto, DeliverySession>(sessionDto);
        _deliverySessionRepositories.Update(session);
        _deliveryOrderServices.UpdateDOBySession(sessionDto);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitTransactionAsync();

        return sessionDto;
    }
    public async Task<DeliverySessionDto> Returned(string deliverySessionCode, DeliverySessionConfirmDto data)
    {
        var session = GetDeliverySessionByCode(deliverySessionCode);

        if (session.Status != SessionStatusEnum.New.ToString() && session.Status != SessionStatusEnum.AConfirmed.ToString())
        {
            throw new DeliverySessionInvalidStatusToSwitchException();
        }

        await _unitOfWork.BeginTransactionAsync();

        var sessionDto = _mapper.Map<DeliverySession, DeliverySessionDto>(session);
        sessionDto.Returned(data.Note);
        session = _mapper.Map<DeliverySessionDto, DeliverySession>(sessionDto);
        _deliverySessionRepositories.Update(session);
        _deliveryOrderServices.UpdateDOBySession(sessionDto);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitTransactionAsync();

        return sessionDto;
    }

    public async Task<DeliverySessionDto> AssignDriverToDOs(AssignDriverDto data)
    {
        var deliveryOrders = _deliveryOrderRepositories.GetAll()
                        .Include(x => x.Childrens)
                        .ThenInclude(x => x.DeliveryOrderLines)
                        .Include(x => x.DeliveryOrderLines)
                        .Where(x =>
                            data.DeliveryOrderCodes.Contains(x.Code) ||
                            (x.ParentCode != null && data.DeliveryOrderCodes.Contains(x.ParentCode))
                        )
                        .ToList();
        if (deliveryOrders.Any(x => x.SessionCode != null || x.Childrens.Any(c => c.SessionCode != null)))
        {
            var codes = deliveryOrders
                        .Where(x => x.SessionCode != null || x.Childrens.Any(c => c.SessionCode != null))
                        .Select(x => x.Code).ToList();
            throw new DeliverySessionDOInAnotherException(codes);
        }

        var driver = _employeeRepositories.GetEmployeeByCode(data.DriverCode);
        if (driver == null || driver.EmployeeType.Equals("driver")==false)
        {
            throw new EmployeeNotFoundException();
        }
        var driverDto = _mapper.Map<Employee, EmployeeDto>(driver);

        var vehicle = _vehicleRepositories.GetVehicleByCode(data.VehicleCode);
        if (vehicle == null || driver.Status != "active")
        {
            throw new VehicleNotFoundException();
        }
        var vehicleDto = _mapper.Map<Vehicle, VehicleDto>(vehicle);
        var station = _stationRepositories.GetStationByCode(data.StationCode);
        if (station == null || station.Status != "active")
        {
            throw new StationNotFoundException();
        }
        var stationDto = _mapper.Map<Station, StationDto>(station);

        await _unitOfWork.BeginTransactionAsync();

        var deliverySessionDto = new DeliverySessionDto();
        var deliveryOrdersDto = _mapper.Map<List<DeliveryOrder>, List<DeliveryOrderDto>>(deliveryOrders);
        deliverySessionDto.SessionType = SessionTypeEnum.Pickup.ToString();
        deliverySessionDto.CreateSessionLinesFromDOs(deliveryOrdersDto);
        deliverySessionDto.CreateSession(null, null);
        deliverySessionDto.AssignToDriver(driverDto);
        deliverySessionDto.AssignToVehicle(vehicleDto);
        deliverySessionDto.AssignToStation(stationDto);

        var deliverySession = _mapper.Map<DeliverySessionDto, DeliverySession>(deliverySessionDto);
        _deliverySessionRepositories.Add(deliverySession);
        _deliveryOrderServices.UpdateDOBySession(deliverySessionDto);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitTransactionAsync();

        return deliverySessionDto;
    }

    public DeliverySession GetDeliverySessionByCode(string code)
    {
        var deliverySession = _deliverySessionRepositories.GetDeliverySessionByCode(code);

        if (deliverySession == null)
        {
            throw new DeliverySessionNotFoundException();
        }

        return deliverySession;
    }

    // private void AssignSessionToDOs(string sessionCode, List<DeliverySessionLineDto> sessionLineDtos)
    // {
    //     var doCodes = new List<string>();
    //     doCodes.AddRange(sessionLineDtos.Where(x => x.DeliveryOrderCode != null).Select(x => x.DeliveryOrderCode).Distinct().ToList());
    //     doCodes.AddRange(sessionLineDtos.Where(x => x.DeliveryOrderParentCode != null).Select(x => x.DeliveryOrderParentCode).Distinct().ToList());
    //     if (doCodes.Count > 0)
    //     {
    //         _deliveryOrderRepositories.AssignSession(doCodes, sessionCode);
    //     }
    // }

    // private void UnassignSessionToDOs(List<DeliverySessionLineDto> sessionLineDtos)
    // {
    //     var doCodes = sessionLineDtos.Select(x => x.DeliveryOrderCode).Distinct().ToList();
    //     if (doCodes.Count > 0)
    //     {
    //         _deliveryOrderRepositories.UnassignSession(doCodes);
    //     }
    // }

    private void ValidateSessions(List<DeliverySessionDto> sessionDtos)
    {
        var sessions = sessionDtos
                        .Select(session => new
                        {
                            DOCodes = session.DeliverySessionLines
                                        .Select(x => x.DeliveryOrderCode)
                                        .Distinct()
                                        .ToList()
                        })
                        .ToList();
        var duplicatedDOs = sessions
                .SelectMany(x => x.DOCodes)
                .GroupBy(x => x)
                .Select(x => new { DOCode = x.Key, Count = x.Count() })
                .ToList();

        if (duplicatedDOs.Any(x => x.Count > 1))
        {
            var codes = duplicatedDOs.Select(x => x.DOCode).ToList();
            throw new DeliverySessionDOInAnotherException(codes);
        }

        sessionDtos.ForEach(x => ValidateSession(x));
    }

    private void ValidateSession(DeliverySessionDto sessionDto)
    {
        if (sessionDto.DeliverySessionLines == null || sessionDto.DeliverySessionLines.Count == 0)
        {
            throw new Exception("Phiên phải có ít nhất một đơn hàng");
        }

        var doCodes = sessionDto.DeliverySessionLines.Select(x => x.DeliveryOrderCode).Distinct().ToList();

        var dosInSession = _deliveryOrderRepositories.GetAll()
                .Where(x => doCodes.Contains(x.Code))
                .Select(x => new
                {
                    DOCode = x.Code,
                    SessionCode = x.SessionCode,
                    SessionStatus = x.Session.Status,
                    TotalDPs = x.Childrens.Count > 0
                        ? x.Childrens.SelectMany(c => c.DeliveryOrderLines).Count()
                        : x.DeliveryOrderLines.Count()
                })
                .ToList();

        // Check DOs are fully
        var dosToValidate = sessionDto?.DeliverySessionLines
                .GroupBy(x => x.DeliveryOrderCode)
                .Select(x => new
                {
                    DOCode = x.Key,
                    TotalDPs = x.Count()
                })
                .ToList();

        dosToValidate?.ForEach(x =>
        {
            var doItem = dosInSession.Where(c => c.DOCode == x.DOCode).FirstOrDefault();
            if (doItem == null || (doItem != null && doItem.TotalDPs != x.TotalDPs))
            {
                throw new DeliverySessionPartiallyDOException();
            }
        });

        // Check DOs is used in other sessions
        var validSessionDOs = dosInSession
            .Where(x => x.SessionCode == null ||
                (x.SessionCode != null && x.SessionStatus == SessionStatusEnum.Cancelled.ToString()) ||
                (sessionDto?.Code != null && x.SessionCode == sessionDto.Code))
            .ToList();
        if (validSessionDOs.Count != dosInSession.Count)
        {
            var codes = validSessionDOs.Select(x => x.DOCode).ToList();
            throw new DeliverySessionDOInAnotherException(codes);
        }
    }
}
