using AutoMapper;
using Databases.Entities;
using Repositories.DeliveryOrderGroupRepository;
using Repositories.DeliveryOrderRepository;
using Services.Helper.Enums;
using Services.Helper.Exceptions.DeliveryOrder;
using Services.Helper.Exceptions.DeliveryOrderGroup;
using Services.Interfaces;
using Services.Models.DeliveryOrder;
using Services.Models.DeliveryOrderGroup;
using Services.Models.Pagination;
using Microsoft.EntityFrameworkCore;
using Databases.Interfaces;

namespace Services.Implementations;

public class DeliveryOrderGroupServices : IDeliveryOrderGroupServices
{
    private readonly IDeliveryOrderGroupRepositories _deliveryOrderGroupRepositories;
    private readonly IDeliveryOrderRepositories _deliveryOrderRepositories;

    private readonly ICommonServices _commonServices;
    private readonly IDeliveryOrderServices _deliveryOrderServices;

    private readonly IMapper _mapper;

    private readonly IUnitOfWork _unitOfWork;
    
    public DeliveryOrderGroupServices(
        IDeliveryOrderGroupRepositories deliveryOrderGroupRepositories,
        IDeliveryOrderRepositories deliveryOrderRepositories, 
        IMapper mapper,
        ICommonServices commonServices,
        IDeliveryOrderServices deliveryOrderServices)
    {
        _deliveryOrderGroupRepositories = deliveryOrderGroupRepositories;
        _deliveryOrderRepositories = deliveryOrderRepositories;
        _mapper = mapper;
        _commonServices = commonServices;
        _deliveryOrderServices = deliveryOrderServices;
        _unitOfWork = _deliveryOrderGroupRepositories.UnitOfWork;
    }

    public async Task<PaginatedResultDto<DeliveryOrderGroupDto>> GetAll(DeliveryOrderGroupQuery queryData)
    {
        var query = _deliveryOrderGroupRepositories.GetAllDeliveryOrderGroups(queryData);
        var result = _commonServices.CreatePaginationDtoResponse<DeliveryOrderGroup, DeliveryOrderGroupDto>(query, queryData);
        return result;
    }

    /// <summary>
    /// Validate if the received delivery orders is invalid
    /// </summary>
    /// <param name="rootDOs"></param>
    /// <param name="deliveryOrdersToUpdate"></param>
    /// <exception cref="DeliveryOrderGroupInvalidException"></exception>
    public void ValidateDeliveryOrders(List<DeliveryOrder> rootDOs, List<string> deliveryOrdersToUpdate)
    {
        var correctDOs = new HashSet<string>();
        
        rootDOs.ForEach(e =>
        {
            var childrenByParent = deliveryOrdersToUpdate.Where(x => e.Childrens.Any(k => x == k.Code));
            
            if (childrenByParent.Count() == e.Childrens.Count)
            {
                correctDOs.UnionWith(childrenByParent);
            }
            else if (deliveryOrdersToUpdate.Contains(e.Code))
            {
                correctDOs.Add(e.Code);
            }
        });

        var errorDOs = deliveryOrdersToUpdate.Except(correctDOs);

        if (errorDOs.Count() > 0)
        {
            throw new DeliveryOrderGroupInvalidException()
            {
                ErrorData = errorDOs
            };
        }
    }
    
    public async Task<DeliveryOrderGroupDto> Create(DeliveryOrderGroupCreationDto deliveryOrderGroupCreationDto)
    {
        await _unitOfWork.BeginTransactionAsync();

        deliveryOrderGroupCreationDto.RandomDeliveryOrderGroupCode();
        var doCodes = deliveryOrderGroupCreationDto.DeliveryOrderCodes.Select(e => e);

        var rootDOs = _deliveryOrderRepositories
            .GetAll()
            .Include(e => e.Childrens)
            .Where(e => (doCodes.Contains(e.Code) || e.Childrens.Any(x => doCodes.Contains(x.Code))) && e.ParentCode == null)
            .ToList();
        
        // Validate before update
        if (rootDOs.Any(e => e.GroupCode != null))
        {
            throw new DeliveryOrderIsInOtherGroupException();
        }
        ValidateDeliveryOrders(rootDOs,  deliveryOrderGroupCreationDto.DeliveryOrderCodes);
        
        // Create new Delivery Order Group
        var newDeliveryOrderGroup = _mapper.Map<DeliveryOrderGroupCreationDto, DeliveryOrderGroup>(deliveryOrderGroupCreationDto);
        _deliveryOrderGroupRepositories.Add(newDeliveryOrderGroup);
        
        // Update Delivery Order Group Code
        rootDOs = await _deliveryOrderServices.UpdateDOGroupCode(rootDOs, newDeliveryOrderGroup);
        _deliveryOrderRepositories.UpdateRange(rootDOs);
        
        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitTransactionAsync();
        
        var result = GetResult(rootDOs, newDeliveryOrderGroup);
        return result;
    }

    public async Task<DeliveryOrderGroupDto> GetOne(string code)
    {
        var deliveryOrderGroup = _deliveryOrderGroupRepositories.GetOneByCode(code);
        if (deliveryOrderGroup == null)
        {
            throw new DeliveryOrderGroupNotFoundException();
        }
        var result = _mapper.Map<DeliveryOrderGroup, DeliveryOrderGroupDto>(deliveryOrderGroup);
        return result;
    }

    public async Task<DeliveryOrderGroupDto> Cancel(string code, DeliveryOrderGroupCancelDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();
        var deliveryOrderGroup = _deliveryOrderGroupRepositories
            .GetAll()
            .Include(e => e.DeliveryOrders)
            .FirstOrDefault(e => e.Code == code);

        if (deliveryOrderGroup == null)
        {
            throw new DeliveryOrderGroupNotFoundException();
        }
        
        if (deliveryOrderGroup.Status == DeliveryOrderGroupStatusEnum.Cancelled.ToString())
        {
            throw new DeliveryOrderGroupCanceledException();
        }
        
        deliveryOrderGroup.Status = DeliveryOrderGroupStatusEnum.Cancelled.ToString();
        deliveryOrderGroup.CancelReason = dto.Reason;
        _deliveryOrderGroupRepositories.Update(deliveryOrderGroup);
        
        foreach (var deliveryOrder in deliveryOrderGroup.DeliveryOrders)
        {
            deliveryOrder.GroupCode = null;
            deliveryOrder.DeliveryOrderGroup = null;
        }
        _deliveryOrderRepositories.UpdateRange(deliveryOrderGroup.DeliveryOrders.ToList());
        
        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitTransactionAsync();
        var result = _mapper.Map<DeliveryOrderGroup, DeliveryOrderGroupDto>(deliveryOrderGroup);
        return result;
    }

    public DeliveryOrderGroupDto GetResult(List<DeliveryOrder> deliveryOrders, DeliveryOrderGroup deliveryOrderGroup)
    {
        var result = _mapper.Map<DeliveryOrderGroup, DeliveryOrderGroupDto>(deliveryOrderGroup);
        var deliveryOrderDtos = _mapper.Map<List<DeliveryOrder>, List<DeliveryOrderDto>>(deliveryOrders);
        result.DeliveryOrders = deliveryOrderDtos;
        return result;
    }

    public List<DeliveryOrder> GetNewDeliveryToUpdate(DeliveryOrderGroupUpdateDto deliveryOrderGroupUpdateDto, List<DeliveryOrder> deliveryOrders)
    {
        var newDeliveryOrderCodes =
            deliveryOrderGroupUpdateDto.DeliveryOrderCodes
                .Where(e => deliveryOrders.All(k => k.Code != e && k.Childrens.All(u => u.Code != e)))
                .ToList();

        var newDeliveryOrders = _deliveryOrderRepositories
            .GetAll()
            .Include(e => e.Childrens)
            .Where(e => newDeliveryOrderCodes.Any(q => q == e.Code))
            .ToList();

        return newDeliveryOrders;
    }

    public async Task<DeliveryOrderGroupDto> Update(DeliveryOrderGroupUpdateDto deliveryOrderGroupUpdateDto, string code)
    {
        await _unitOfWork.BeginTransactionAsync();

        var deliveryOrderGroup = _deliveryOrderGroupRepositories
            .GetAll()
            .Include(e => e.DeliveryOrders.Where(e => e.ParentCode == null))
            .ThenInclude(e => e.Childrens)
            .FirstOrDefault(e => e.Code == code);

        if (deliveryOrderGroup == null)
        {
            throw new DeliveryOrderGroupNotFoundException();
        }
        
        var rootDOs = deliveryOrderGroup.DeliveryOrders
            .Where(e => 
                deliveryOrderGroupUpdateDto.DeliveryOrderCodes.Any(k => k == e.Code || k == e.ParentCode))
            .ToList();

        var deliveryOrderToUpdate = new List<string>(deliveryOrderGroupUpdateDto.DeliveryOrderCodes);
        
        // Validate before update
        if (rootDOs.Any(e => e.GroupCode != null && e.GroupCode != deliveryOrderGroup.Code))
        {
            throw new DeliveryOrderIsInOtherGroupException();
        }
        ValidateDeliveryOrders(rootDOs, deliveryOrderToUpdate);

        var newDeliveryOrders = GetNewDeliveryToUpdate(deliveryOrderGroupUpdateDto, rootDOs);
        rootDOs.AddRange(newDeliveryOrders);  
        
        var removeDeliveryOrders = deliveryOrderGroup.DeliveryOrders
            .Where(e => 
                !deliveryOrderGroupUpdateDto.DeliveryOrderCodes
                    .Any(k => k == e.Code || e.Childrens.Any(u => u.Code == k)))
            .ToList();
        
        if (newDeliveryOrders.Any())
        {
            rootDOs = await _deliveryOrderServices.UpdateDOGroupCode(rootDOs, deliveryOrderGroup);
            _deliveryOrderRepositories.UpdateRange(rootDOs);
        }

        if (removeDeliveryOrders.Any())
        {
            removeDeliveryOrders = await _deliveryOrderServices.RemoveDOFromGroup(removeDeliveryOrders);
            _deliveryOrderRepositories.UpdateRange(removeDeliveryOrders);
        }
        
        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitTransactionAsync();

        var result = GetResult(rootDOs, deliveryOrderGroup);
        return result;
    }
}