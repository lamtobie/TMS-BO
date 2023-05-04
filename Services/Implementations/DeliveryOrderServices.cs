using AutoMapper;
using Databases.Entities;
using Repositories.DeliveryOrderLineRepository;
using Repositories.DeliveryOrderRepository;
using Repositories.DeliveryPackageGroupRepository;
using Repositories.DeliveryPackageRepository;
using Services.Helper.Enums;
using Services.Helper.Exceptions.DeliveryOrder;
using Services.Helper.Exceptions.DeliveryOrderGroup;
using Services.Interfaces;
using Services.Models.Address;
using Services.Models.DataAttribute;
using Services.Models.DeliveryOrder;
using Services.Models.DeliverySession;
using Services.Models.Employee;
using Services.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using Databases.Interfaces;
using Services.Helper.Exceptions.Station;
using Services.Models.Station;

namespace Services.Implementations;

public class DeliveryOrderServices : IDeliveryOrderServices
{
    private readonly ICommonServices _commonServices;
    private readonly IDeliveryOrderLineServices _deliveryOrderLineServices;

    private readonly IDeliveryOrderRepositories _deliveryOrderRepositories;
    private readonly IDeliveryPackageRepositories _deliveryPackageRepositories;
    private readonly IDeliveryPackageGroupRepositories _deliveryPackageGroupRepositories;
    private readonly IDeliveryOrderLineRepositories _deliveryOrderLineRepositories;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeliveryOrderServices(
        ICommonServices commonServices,
        IDeliveryOrderLineServices deliveryOrderLineServices,
        IDeliveryOrderRepositories deliveryOrderRepositories,
        IDeliveryPackageRepositories deliveryPackageRepositories,
        IDeliveryPackageGroupRepositories deliveryPackageGroupRepositories,
        IDeliveryOrderLineRepositories deliveryOrderLineRepositories,
        IMapper mapper
        )
    {
        _commonServices = commonServices;
        _deliveryOrderLineServices = deliveryOrderLineServices;
        _deliveryOrderRepositories = deliveryOrderRepositories;
        _unitOfWork = deliveryOrderRepositories.UnitOfWork;
        _deliveryPackageRepositories = deliveryPackageRepositories;
        _deliveryPackageGroupRepositories = deliveryPackageGroupRepositories;
        _deliveryOrderLineRepositories = deliveryOrderLineRepositories;
        _mapper = mapper;
    }

    private string RandomDOCode()
    {
        return "DO" + Guid.NewGuid().ToString("n").Substring(0, 8).ToUpper();
    }
    public async Task<DeliveryOrderDto> CreateDO(DeliveryOrderDto orderCreationDto)
    {
        var existedOrder = _deliveryOrderRepositories.GetDeliveryOrderByCode(orderCreationDto.Code);
        if (existedOrder != null)
        {
            throw new DeliveryOrderDuplicatedCodeException();
        }
        var order = _mapper.Map<DeliveryOrderDto, DeliveryOrder>(orderCreationDto);
        _deliveryOrderRepositories.Create(order);

        var orderDto = _mapper.Map<DeliveryOrder, DeliveryOrderDto>(order);
        return orderDto;
    }
    public async Task<PaginatedResultDto<DeliveryOrder>> GetAll(DeliveryOrderQuery query)
    {
        var queryable = _deliveryOrderRepositories.GetAllDeliveryOrders(query);
        var result = _commonServices.CreatePaginationResponse<DeliveryOrder>(queryable, query);
        return result;
    }

    public async Task<PaginatedResultDto<SearchPersonResponseDto>> GetDeliveryOrderReponsibility(SearchPersonQuery query)
    {
        var queryable = _deliveryOrderRepositories.GetAllDeliveryOrderReponsibility(query);
        var result = _commonServices.CreatePaginationDtoResponse<DeliveryOrder, SearchPersonResponseDto>(queryable, query);
        return result;
    }

    public async Task<PaginatedResultDto<DeliveryOrderTreeResultDto>> GetTree(DeliveryOrderQuery query)
    {
        var queryable = _deliveryOrderRepositories.GetAllDeliveryOrders(query);
        queryable = queryable.Where(x => x.ParentCode == null);
        var queryableResult = queryable
                        .Select(x => new DeliveryOrderTreeResultDto()
                        {
                            Code = x.Code,
                            ParentCode = x.ParentCode,
                            ReferenceCode = x.Childrens.Count > 0
                                            ? String.Join(", ", x.Childrens.Select(c => c.ReferenceCode).ToList())
                                            : x.ReferenceCode,
                            TotalItems = x.Childrens.Count > 0 ? x.Childrens.Sum(c => c.TotalItems) : x.TotalItems,
                            Status = x.Status,
                            StartAddress = _mapper.Map<Address, AddressDto>(x.StartAddress),
                            EndAddress = _mapper.Map<Address, AddressDto>(x.Childrens.First().EndAddress),
                            EndContactPerson = x.Childrens.Count > 0
                                            ? String.Join(", ", x.Childrens.Select(c => c.EndContactPerson).ToList()): x.EndContactPerson,
                            EndContactPhone = x.Childrens.Count > 0
                                            ? String.Join(", ", x.Childrens.Select(c => c.EndContactPhone).ToList()) : x.EndContactPhone,
                            EndStationCode = x.EndStationCode,
                            StartContactPerson = x.StartContactPerson,
                            StartContactPhone = x.StartContactPhone,
                            ExpectedStartTime = x.ExpectedStartTime,
                            ExpectedTimeConsumed= x.Childrens.Count == 1 ? x.Childrens.First().ExpectedTimeConsumed :x.ExpectedTimeConsumed,
                            ExpectedArrivalTime = x.Childrens.Count == 1 ? x.Childrens.First().ExpectedArrivalTime : x.ExpectedArrivalTime,
                            EndNote = x.Childrens.Count == 1 ? x.Childrens.First().EndNote : x.EndNote,
                            StartNote = x.StartNote,
                            ThreePLTeam = x.Childrens.Count == 1 ? x.Childrens.First().ThreePLTeam : x.ThreePLTeam,
                            CodAmount = x.Childrens.Count > 0 ? x.Childrens.Sum(x => x.CodAmount) : x.CodAmount,
                            DriverCode = x.DriverCode,
                            Driver = _mapper.Map<Employee, EmployeeDto>(x.Driver),
                            CoordinatorCode = x.CoordinatorCode,
                            Coordinator = _mapper.Map<Employee, EmployeeDto>(x.Coordinator),
                            SessionCode = x.SessionCode,
                            GroupCode = x.GroupCode,
                            NumberOfTransit = x.Childrens.Count(c => c.IsToCustomer == null || c.IsToCustomer == false),
                            SourceBy = x.SourceBy,
                            CreatedAt = x.CreatedAt,
                            ActualTimeConsumed = x.ActualTimeConsumed,
                            ActualStartTime = x.ActualStartTime,
                            ActualArrivalTime = x.ActualArrivalTime,
                            StartStationCode = x.StartStationCode,
                            Childrens = x.Childrens,
                        })
                        .AsQueryable();
        var result = _commonServices.CreatePaginationResponse<DeliveryOrderTreeResultDto>(queryableResult, query);
        return result;
    }

    public async Task<DeliveryOrderDto> GetOneByCode(string code)
    {
        var deliveryOrder = GetDeliveryOrderByCode(code);

        var result = _mapper.Map<DeliveryOrder, DeliveryOrderDto>(deliveryOrder);

        return result;
    }

    public async Task<DeliveryOrderDto> CreateManyDropoff(DeliveryOrderManyDropoffCreationDto data)
    {
        if (data.DropoffInfo.Count == 0)
        {
            throw new Exception("abc");
        }

        await _unitOfWork.BeginTransactionAsync();

        var parentDODto = new DeliveryOrderDto();
        parentDODto.Code = RandomDOCode();
        parentDODto.StartAddress = data.PickupInfo.StartAddress;
        parentDODto.StartContactPerson = data.PickupInfo.StartContactPerson;
        parentDODto.StartContactPhone = data.PickupInfo.StartContactPhone;
        parentDODto.StartNote = data.PickupInfo.StartNote;
        parentDODto.ExpectedStartTime = data.PickupInfo.ExpectedStartTime;
        parentDODto.IsToCustomer = data.PickupInfo.StartStationCode == null;

        // Create dropoff DOs
        var deliverOrders = new List<DeliveryOrderDto>();
        data.DropoffInfo.ForEach(dropoffInfo =>
        {
            var dropoffDO = new DeliveryOrderDto();

            dropoffDO.Code = RandomDOCode();
            dropoffDO.ParentCode = parentDODto.Code;
           
            dropoffDO.StartAddress = parentDODto.StartAddress;
            dropoffDO.StartContactPerson = parentDODto.StartContactPerson;
            dropoffDO.StartContactPhone = parentDODto.StartContactPhone;
            dropoffDO.StartNote = parentDODto.StartNote;
            dropoffDO.ExpectedStartTime = parentDODto.ExpectedStartTime;

            dropoffDO.EndAddress = dropoffInfo.EndAddress;
            dropoffDO.EndContactPerson = dropoffInfo.EndContactPerson;
            dropoffDO.EndContactPhone = dropoffInfo.EndContactPhone;
            dropoffDO.EndNote = dropoffInfo.EndNote;

            dropoffDO.ExpectedArrivalTime = dropoffInfo.ExpectedArrivalTime;
            dropoffDO.ExpectedTimeConsumed = dropoffInfo.ExpectedTimeConsumed;
            dropoffDO.ReferenceCode = dropoffInfo.ReferenceCode ?? dropoffDO.Code;
            dropoffDO.ThreePLTeam = dropoffInfo.ThreePLTeam;
            dropoffDO.ProductType = dropoffInfo.ProductType;
            dropoffDO.TotalItems = dropoffInfo.TotalItems;
            dropoffDO.Weight = dropoffInfo.Weight;
            dropoffDO.CodAllowed = dropoffInfo.CodAllowed;
            dropoffDO.CodAmount = dropoffInfo.CodAmount;
            dropoffDO.CodMethod = dropoffInfo.CodMethod;
            dropoffDO.Additional = dropoffInfo.Additional;
            dropoffDO.DeliveryOrderLines = dropoffInfo.DeliveryOrderLines;

            dropoffDO.IsToCustomer = dropoffInfo.EndStationCode == null;

            dropoffDO.DeliveryOrderLines?.ForEach(doLine =>
            {
                _deliveryOrderLineServices.InitData(doLine, dropoffDO);
            });

            deliverOrders.Add(dropoffDO);
        });

        var dropoffDOs = _mapper.Map<List<DeliveryOrderDto>, List<DeliveryOrder>>(deliverOrders);
        _deliveryOrderRepositories.AddRange(dropoffDOs);

        var parentDO = _mapper.Map<DeliveryOrderDto, DeliveryOrder>(parentDODto);
        _deliveryOrderRepositories.Add(parentDO);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitTransactionAsync();

        return parentDODto;
    }

    public async Task<DeliveryOrderDto> CreateManyInTransit(DeliveryOrderInTransitCreationDto data)
    {
        await _unitOfWork.BeginTransactionAsync();

        if (data.Subs?.Count == 0)
        {
            throw new Exception("abc");
        }

        var parentDODto = new DeliveryOrderDto();
        parentDODto.Code = RandomDOCode();
        parentDODto.Status = DeliveryOrderStatusEnum.New.ToString();
        parentDODto.ExpectedStartTime = data.Data.ExpectedStartTime;
        parentDODto.ExpectedArrivalTime = data.Data.ExpectedArrivalTime;
        parentDODto.ExpectedTimeConsumed = data.Data.ExpectedTimeConsumed;
        parentDODto.ReferenceCode = data.Data.ReferenceCode ?? parentDODto.Code;
        parentDODto.ThreePLTeam = data.Data.ThreePLTeam;
        parentDODto.ProductType = data.Data.ProductType;
        parentDODto.TotalItems = data.Data.TotalItems;
        parentDODto.Weight = data.Data.Weight;
        parentDODto.CodAllowed = data.Data.CodAllowed;
        parentDODto.CodAmount = data.Data.CodAmount;
        parentDODto.CodMethod = data.Data.CodMethod;
        parentDODto.StartAddress = data.Data.StartAddress;
        parentDODto.StartContactPerson = data.Data.StartContactPerson;
        parentDODto.StartContactPhone = data.Data.StartContactPhone;
        parentDODto.StartNote = data.Data.StartNote;
        parentDODto.EndAddress = data.Data.EndAddress;
        parentDODto.EndContactPerson = data.Data.EndContactPerson;
        parentDODto.EndContactPhone = data.Data.EndContactPhone;
        parentDODto.EndNote = data.Data.EndNote;
        parentDODto.Additional = data.Data.Additional;
        parentDODto.DeliveryOrderLines = data.Data.DeliveryOrderLines;
        parentDODto.IsToCustomer = data.Data.EndStationCode == null;
        parentDODto.DeliveryOrderLines?.ForEach(doLine =>
        {
            _deliveryOrderLineServices.InitData(doLine, parentDODto);
        });

        // Create sub DOs
        if (data.Subs?.Count > 0)
        {
            var deliverOrders = new List<DeliveryOrderDto>();
            var order = 0;
            data.Subs.ForEach(sub =>
            {
                order++;

                var subDO = new DeliveryOrderDto();

                subDO.Code = RandomDOCode();
                subDO.ParentCode = parentDODto.Code;

                subDO.TransitOrder = order;
                subDO.StartStationCode = sub.StartStationCode;
                subDO.StartAddress = sub.StartAddress;
                subDO.StartContactPerson = sub.StartContactPerson;
                subDO.StartContactPhone = sub.StartContactPhone;
                subDO.StartNote = sub.StartNote;
                subDO.ExpectedStartTime = sub.ExpectedStartTime;
                subDO.EndStationCode = sub.EndStationCode;
                subDO.EndAddress = sub.EndAddress;
                subDO.EndContactPerson = sub.EndContactPerson;
                subDO.EndContactPhone = sub.EndContactPhone;
                subDO.EndNote = sub.EndNote;
                subDO.ExpectedArrivalTime = sub.ExpectedArrivalTime;
                subDO.ExpectedTimeConsumed = sub.ExpectedTimeConsumed;
                subDO.ReferenceCode = sub.ReferenceCode ?? subDO.Code;
                subDO.ThreePLTeam = sub.ThreePLTeam;
                subDO.ProductType = sub.ProductType;
                subDO.TotalItems = sub.TotalItems;
                subDO.Weight = sub.Weight;
                subDO.CodAllowed = sub.CodAllowed;
                subDO.CodAmount = sub.CodAmount;
                subDO.CodMethod = sub.CodMethod;
                subDO.Additional = sub.Additional;
                subDO.DeliveryOrderLines = sub.DeliveryOrderLines;

                subDO.IsToCustomer = sub.EndStationCode == null;

                subDO.DeliveryOrderLines?.ForEach(doLine =>
                {
                    _deliveryOrderLineServices.InitData(doLine, subDO);
                });

                deliverOrders.Add(subDO);
            });

            parentDODto.NumberOfTransit = deliverOrders.Count(x => x.IsToCustomer == null || x.IsToCustomer == false);

            var subDOs = _mapper.Map<List<DeliveryOrderDto>, List<DeliveryOrder>>(deliverOrders);
            _deliveryOrderRepositories.AddRange(subDOs);
        }

        var parentDO = _mapper.Map<DeliveryOrderDto, DeliveryOrder>(parentDODto);
        _deliveryOrderRepositories.Add(parentDO);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitTransactionAsync();

        return parentDODto;
    }

    public DeliveryOrder CreateChildDO(DeliveryOrderDto data, DeliveryOrder parentDO)
    {
        data.Code = RandomDOCode();
        data.ParentCode = parentDO.Code;
        data.ReferenceCode = data.ReferenceCode ?? data.Code;
        data.ExpectedArrivalTime = parentDO.ExpectedArrivalTime;
        data.StartAddress = _mapper.Map<Address, AddressDto>(parentDO.StartAddress);
        data.StartContactPerson = parentDO.StartContactPerson;
        data.StartContactPhone = parentDO.StartContactPhone;
        data.StartNote = parentDO.StartNote;
        data.Status = DeliveryOrderStatusEnum.New.ToString();

        data.DeliveryOrderLines?.ForEach(doLine =>
        {
            _deliveryOrderLineServices.InitData(doLine, data);
        });

        var child = _mapper.Map<DeliveryOrderDto, DeliveryOrder>(data);

        _deliveryOrderRepositories.Add(child);

        return child;
    }

    public async Task<DeliveryOrderDto> Update(string deliveryOrderCode, DeliveryOrderDto data)
    {
        await _unitOfWork.BeginTransactionAsync();

        var deliveryOrder = GetDeliveryOrderByCode(data.Code);

        // Update pickup order
        if (deliveryOrder.ParentCode == null)
        {
            if (deliveryOrder.StartAddress != null && data.StartAddress != null)
            {
                deliveryOrder.StartAddress.BlockAddress = data.StartAddress.BlockAddress;
                deliveryOrder.StartAddress.ClusterAddress = data.StartAddress.BlockAddress;
                deliveryOrder.StartAddress.ClusterAddress = data.StartAddress.ClusterAddress;
                deliveryOrder.StartAddress.QuarterAddress = data.StartAddress.QuarterAddress;
                deliveryOrder.StartAddress.SubQuarterAddress = data.StartAddress.SubQuarterAddress;
                deliveryOrder.StartAddress.Text = data.StartAddress.Text;
                deliveryOrder.StartAddress.SlicCode = data.StartAddress.SlicCode;
                deliveryOrder.StartAddress.SlicLabel = data.StartAddress.SlicLabel;
                deliveryOrder.StartAddress.Lat = data.StartAddress.Lat;
                deliveryOrder.StartAddress.Long = data.StartAddress.Long;
                deliveryOrder.StartAddress.SlicRegion = data.StartAddress.SlicRegion;
                deliveryOrder.StartAddress.SlicLevel = data.StartAddress.SlicLevel;
                deliveryOrder.StartAddress.SlicWard = data.StartAddress.SlicWard;
                deliveryOrder.StartAddress.SlicDistrict = data.StartAddress.SlicDistrict;
                deliveryOrder.StartAddress.SlicProvince = data.StartAddress.SlicProvince;
            }
            deliveryOrder.StartContactPerson = data.StartContactPerson;
            deliveryOrder.StartContactPhone = data.StartContactPhone;
            deliveryOrder.StartNote = data.StartNote;
            deliveryOrder.StartStationCode = data.StartStationCode;
            deliveryOrder.ExpectedStartTime = data.ExpectedStartTime;
            deliveryOrder.Status = data.Status;
        }

        // Update dropoff orders
        if (data?.Childrens?.Count == 0)
        {
            // Remove all dropoff orders
            // _deliveryOrderRepositories.DeleteRange(deliveryOrder.Childrens.ToList());
            _deliveryOrderRepositories.MarkDeletedRange(deliveryOrder.Childrens.ToList());
        }
        else
        {
            // Update dropoff orders
            var updateChildrens = data.Childrens.Where(x => deliveryOrder.Childrens.Any(u => u.Code == x.Code)).ToList();
            if (updateChildrens.Count > 0)
            {
                updateChildrens.ForEach((childrenDO) =>
                {
                    var sourceChildDO = deliveryOrder.Childrens.Where(x => x.Code == childrenDO.Code).FirstOrDefault();
                    _deliveryOrderLineServices.Update(sourceChildDO, childrenDO);
                    UpdateChildrenDO(sourceChildDO, deliveryOrder, childrenDO);
                });
            }

            // Remove dropoff orders
            var removeChildrens = deliveryOrder.Childrens.Where(x => data.Childrens.All(u => u.Code != x.Code)).ToList();
            if (removeChildrens.Count > 0)
            {
                // _deliveryOrderRepositories.DeleteRange(removeChildrens);
                _deliveryOrderRepositories.MarkDeletedRange(removeChildrens);
            }

            // Add new dropoff orders
            var newChildrens = data.Childrens.Where(x => string.IsNullOrEmpty(x.Code)).ToList();
            if (newChildrens.Count > 0)
            {
                newChildrens.ForEach(x => CreateChildDO(x, deliveryOrder));
            }
        }

        // TODO: Update parent
        if (deliveryOrder.ParentCode != null)
        {

        }
        _deliveryOrderRepositories.Update(deliveryOrder);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitTransactionAsync();

        return await GetOneByCode(deliveryOrderCode);
    }

    public void UpdateDOBySession(DeliverySessionDto sessionDto)
    {
        var doCodes = sessionDto.GetDOCodes();
        var dos = _deliveryOrderRepositories.GetAll().Where(x => doCodes.Contains(x.Code)).ToList();
        var dosDto = _mapper.Map<List<DeliveryOrder>, List<DeliveryOrderDto>>(dos);
        dosDto.ForEach(doDto =>
        {
            doDto.AssignToSession(sessionDto);
            doDto.DriverCode = sessionDto.DriverCode;
            doDto.CoordinatorCode = sessionDto.CoordinatorCode;
            doDto.EndStationCode = sessionDto.EndStationCode;
            doDto.StartStationCode= sessionDto.StartStationCode;
            var doStatus = sessionDto.ComputeDOStatus();
            if (!string.IsNullOrEmpty(doStatus))
            {
                doDto.Status = doStatus;
            }
        });
        dos = _mapper.Map<List<DeliveryOrderDto>, List<DeliveryOrder>>(dosDto);
        _deliveryOrderRepositories.UpdateRange(dos);
    }

    public DeliveryOrder UpdateChildrenDO(DeliveryOrder childrenDO, DeliveryOrder parentDO, DeliveryOrderDto dataToUpdate)
    {
        childrenDO.ReferenceCode = dataToUpdate.ReferenceCode ?? childrenDO.Code;
        childrenDO.ExpectedStartTime = dataToUpdate.ExpectedStartTime;
        childrenDO.ExpectedArrivalTime = dataToUpdate.ExpectedArrivalTime;
        childrenDO.ThreePLTeam = dataToUpdate.ThreePLTeam;
        childrenDO.ProductType = dataToUpdate.ProductType;
        childrenDO.TotalItems = dataToUpdate.TotalItems;
        childrenDO.Weight = dataToUpdate.Weight;
        childrenDO.CodAllowed = dataToUpdate.CodAllowed;
        childrenDO.CodAmount = dataToUpdate.CodAmount;
        childrenDO.CodMethod = dataToUpdate.CodMethod;
        childrenDO.StartContactPerson = parentDO.StartContactPerson;
        childrenDO.StartContactPhone = parentDO.StartContactPhone;
        childrenDO.StartNote = parentDO.StartNote;
        if (childrenDO.EndAddress != null && dataToUpdate.EndAddress != null)
        {
            childrenDO.EndAddress.BlockAddress = dataToUpdate.EndAddress.BlockAddress;
            childrenDO.EndAddress.ClusterAddress = dataToUpdate.EndAddress.BlockAddress;
            childrenDO.EndAddress.ClusterAddress = dataToUpdate.EndAddress.ClusterAddress;
            childrenDO.EndAddress.QuarterAddress = dataToUpdate.EndAddress.QuarterAddress;
            childrenDO.EndAddress.SubQuarterAddress = dataToUpdate.EndAddress.SubQuarterAddress;
            childrenDO.EndAddress.Text = dataToUpdate.EndAddress.Text;
            childrenDO.EndAddress.SlicCode = dataToUpdate.EndAddress.SlicCode;
            childrenDO.EndAddress.SlicLabel = dataToUpdate.EndAddress.SlicLabel;
            childrenDO.EndAddress.Lat = dataToUpdate.EndAddress.Lat;
            childrenDO.EndAddress.Long = dataToUpdate.EndAddress.Long;
            childrenDO.EndAddress.SlicRegion = dataToUpdate.EndAddress.SlicRegion;
            childrenDO.EndAddress.SlicLevel = dataToUpdate.EndAddress.SlicLevel;
            childrenDO.EndAddress.SlicWard = dataToUpdate.EndAddress.SlicWard;
            childrenDO.EndAddress.SlicDistrict = dataToUpdate.EndAddress.SlicDistrict;
            childrenDO.EndAddress.SlicProvince = dataToUpdate.EndAddress.SlicProvince;
        }
        childrenDO.EndContactPerson = dataToUpdate.EndContactPerson;
        childrenDO.EndContactPhone = dataToUpdate.EndContactPhone;
        childrenDO.EndNote = dataToUpdate.EndNote;
        childrenDO.Status = dataToUpdate.Status;
        childrenDO.Additional = _mapper.Map<DataAttributeDto[], DataAttribute[]>(dataToUpdate.Additional);

        _deliveryOrderRepositories.Update(childrenDO);

        return childrenDO;
    }

    public async Task<DeliveryOrderDto> ChangeStatus(string deliveryOrderCode, DeliveryOrderUpdateStatusDto data)
    {
        await _unitOfWork.BeginTransactionAsync();

        var deliveryOrder = GetDeliveryOrderByCode(deliveryOrderCode);

        deliveryOrder.Status = data.Status;

        _deliveryOrderRepositories.Update(deliveryOrder);

        await _unitOfWork.SaveChangesAsync();

        await _unitOfWork.CommitTransactionAsync();

        return await GetOneByCode(deliveryOrderCode);
    }

    public async Task<List<DeliveryOrderDto>> ScanCode(List<string> codes)
    {
        var result = _deliveryOrderRepositories.GetAll()
            .Include(x => x.Session).ThenInclude(x => x.Driver)
            .Include(x => x.Session).ThenInclude(x => x.Coordinator)
            .Include(x => x.DeliveryOrderLines).ThenInclude(x => x.DeliveryPackage)
            .Include(x => x.Childrens).ThenInclude(x => x.DeliveryOrderLines).ThenInclude(x => x.DeliveryPackage)
            .Where(x => codes.Contains(x.Code))
            .ToList();

        var res = _mapper.Map<List<DeliveryOrder>, List<DeliveryOrderDto>>(result);

        return res;
    }

    public DeliveryOrder GetDeliveryOrderByCode(string code)
    {
        var deliveryOrder = _deliveryOrderRepositories.GetDeliveryOrderByCode(code);

        if (deliveryOrder == null)
        {
            throw new DeliveryOrderNotFoundException();
        }

        return deliveryOrder;
    }

    public DeliveryOrder Delete(string code)
    {
        GetDeliveryOrderByCode(code);
        return _deliveryOrderRepositories.DeleteDeliveryOrder(code);
    }

    public void RemoveDOFromGroup(DeliveryOrder deliveryOrder)
    {
        deliveryOrder.GroupCode = null;
        deliveryOrder.DeliveryOrderGroup = null;
        if (deliveryOrder.Childrens != null && deliveryOrder.Childrens.Count > 0)
        {
            deliveryOrder.Childrens.ToList().ForEach(e =>
            {
                RemoveDOFromGroup(e);
            });
        }
    }

    public async Task<List<DeliveryOrder>> RemoveDOFromGroup(List<DeliveryOrder> deliveryOrders)
    {
        List<Task> taskList = new List<Task>();
        deliveryOrders.ForEach(e =>
        {
            taskList.Add(Task.Run(() => RemoveDOFromGroup(e)));
        });
        await Task.WhenAll(taskList);
        return deliveryOrders;
    }

    public async Task UpdateDOGroupCode(DeliveryOrder deliveryOrder, DeliveryOrderGroup deliveryOrderGroup)
    {
        deliveryOrder.GroupCode = deliveryOrderGroup.Code;
        if (deliveryOrder.Childrens == null || deliveryOrder.Childrens.Count == 0)
        {
            return;
        }

        foreach (var deliveryOrderChildren in deliveryOrder.Childrens)
        {
            UpdateDOGroupCode(deliveryOrderChildren, deliveryOrderGroup);
        }
    }

    /// <summary>
    /// Add Delivery Order to Delivery Order Group
    /// </summary>
    /// <param name="deliveryOrders">Delivery Orders to be added</param>
    /// <param name="deliveryOrderGroup">Delivery order group to be received delivery orders</param>
    public async Task<List<DeliveryOrder>> UpdateDOGroupCode(List<DeliveryOrder> deliveryOrders, DeliveryOrderGroup deliveryOrderGroup)
    {
        List<Task> taskList = new List<Task>();
        deliveryOrders.ForEach(e =>
        {
            taskList.Add(Task.Run(() => UpdateDOGroupCode(e, deliveryOrderGroup)));
        });
        await Task.WhenAll(taskList);
        return deliveryOrders;
    }
}