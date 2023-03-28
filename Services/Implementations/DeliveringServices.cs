using AutoMapper;
using Databases.Entities;
using Repositories.DeliveryOrderRepository;
using Repositories.DeliverySessionLineRepository;
using Repositories.DeliverySessionRepository;
using Services.Helper.Enums;
using Services.Helper.Exceptions.DeliveryOrder;
using Services.Helper.Exceptions.DeliverySession;
using Services.Helper.Utils;
using Services.Interfaces;
using Services.Models.Delivering.DeliveryOrder;
using Services.Models.Delivering.DeliveryOrder.ActionDelivering;
using Services.Models.Delivering.Session;
using Services.Models.Delivering.Session.SessionAction;
using Services.Models.Delivery.Session;
using Services.Models.DeliverySession;
using Microsoft.EntityFrameworkCore;
using Databases.Interfaces;

namespace Services.Implementations;

public class DeliveringServices : IDeliveringServices
{
    private readonly ICommonServices _commonServices;
    private readonly IDeliverySessionServices _deliverySessionServices;
    
    private readonly IDeliveryOrderRepositories _deliveryOrderRepositories;
    private readonly IDeliverySessionRepositories _deliverySessionRepositories;

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public DeliveringServices(
        ICommonServices commonServices,
        IDeliverySessionServices deliverySessionServices,
        IDeliveryOrderRepositories deliveryOrderRepositories, 
        IDeliverySessionRepositories deliverySessionRepositories,
        IMapper mapper)
    {
        _commonServices = commonServices;
        _deliveryOrderRepositories = deliveryOrderRepositories;
        _deliverySessionRepositories = deliverySessionRepositories;
        _mapper = mapper;
        _deliverySessionServices = deliverySessionServices;
        _unitOfWork = _deliveryOrderRepositories.UnitOfWork;
    }
    
    public async Task UpdateDeliveryOrderStatus(ICollection<DeliveryOrder> deliveryOrder, string status)
    {
        foreach (var deliveryOrderChildren in deliveryOrder)
        {
            await UpdateDeliveryOrderStatus(deliveryOrderChildren, status);
        }
    }

    public async Task UpdateDeliveryOrderStatus(DeliveryOrder deliveryOrder, string status)
    {
        deliveryOrder.Status = status;
        if (deliveryOrder.Childrens != null && deliveryOrder.Childrens.Count > 0)
        {
            foreach (var deliveryOrderChildren in deliveryOrder.Childrens)
            {
                await UpdateDeliveryOrderStatus(deliveryOrderChildren, status);
            }
        }
    }

    public void UnAssignSession(DeliveryOrder deliveryOrder, DeliveryOrder updatedChild)
    {
        updatedChild.Session = null;
        updatedChild.SessionCode = null;

        if (deliveryOrder.Childrens.All(e => e.SessionCode == null))
        {
            deliveryOrder.SessionCode = null;
            if (deliveryOrder.Session != null)
            {
                deliveryOrder.Session = null;
            }
        }
    }

    public async Task<DeliveringDeliveryOrderDto> UpdateDeliveryOrderStatus(string code, dynamic dto) {
        await _unitOfWork.BeginTransactionAsync();
        var deliveryOrder = _deliveryOrderRepositories
            .GetAll()
            .Include(e => e.Session)
            .Include(e => e.Driver)
            .Include(e => e.DeliveryOrderLines)
            .Include(e => e.Childrens)
            .Where(e => e.ParentCode == null)
            .FirstOrDefault(e => e.Childrens.Any(e => e.Code == code));

        var session = deliveryOrder.Session;

        var children = deliveryOrder.Childrens.FirstOrDefault(e => e.Code == code);
        
        var statusPayload = JsonConvertUtils.ConvertDynamicToObject<DeliveringActionDto>(dto);
        
        if (statusPayload.Status == DeliveryOrderStatusEnum.DeliveringToClient.ToString())
        {
            await StartDelivering(deliveryOrder, session);
            _deliverySessionRepositories.Update(session);
            _deliveryOrderRepositories.Update(deliveryOrder);
        }
        else if (deliveryOrder.Status == DeliveryOrderStatusEnum.DeliveringToClient.ToString())
        {
            if (statusPayload.Status == DeliveryOrderStatusEnum.DeliveredToClientSuccessful.ToString())
            {
                await UpdateSuccessfulDelivery(children, session, JsonConvertUtils.ConvertDynamicToObject<DeliveringSuccessfulDto>(dto));
            }
            else if (statusPayload.Status == DeliveryOrderStatusEnum.DeliveryDelay.ToString())
            {
                await UpdateDelayedDelivery(children, session, JsonConvertUtils.ConvertDynamicToObject<DeliveringDelayedDto>(dto));
            }
            else if (statusPayload.Status == DeliveryOrderStatusEnum.DeliveredToClientFailure.ToString())
            {
                await UpdateFailedDelivery(children, session, JsonConvertUtils.ConvertDynamicToObject<DeliveringFailedDto>(dto));
            }
            else
            {
                throw new DeliveryOrderStatusNotFoundException();
            }

            if (session != null)
            {
                _deliverySessionRepositories.Update(session);
                UnAssignSession(deliveryOrder, children);
            }

            _deliveryOrderRepositories.Update(deliveryOrder);
        }
        
        var result = _mapper.Map<DeliveryOrder, DeliveringDeliveryOrderDto>(deliveryOrder);
   
        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitTransactionAsync();
        return result;
    }

    public async Task UpdateChildStatus(DeliveryOrder deliveryOrder, string status)
    {
        deliveryOrder.Status = status;
        deliveryOrder.Parent.Status = status;
    }

    public async Task StartDelivering(DeliveryOrder deliveryOrder, DeliverySession? session)
    {
        await UpdateDeliveryOrderStatus(deliveryOrder, DeliveryOrderStatusEnum.DeliveringToClient.ToString());
        if (session != null)
        {
            session.Status = SessionStatusEnum.HandedOver.ToString();
        }
    }
    
    public async Task UpdateDelayedDelivery(DeliveryOrder deliveryOrder, DeliverySession? session, DeliveringDelayedDto dto)
    {
        deliveryOrder.ExpectedArrivalTime = dto.ExpectedArrivalTime;
        deliveryOrder.Reason = dto.Reason;
        deliveryOrder.Note = dto.Note;
        var status = dto.Status;
        await UpdateChildStatus(deliveryOrder, status);
        if (session != null)
        {
            session.Status = SessionStatusEnum.HandOverFailed.ToString();
        }
    }

    public async Task UpdateSuccessfulDelivery(DeliveryOrder deliveryOrder, DeliverySession? session, DeliveringSuccessfulDto dto)
    {
        deliveryOrder.Evidence = dto.Evidence;
        deliveryOrder.CODReceived = dto.CODReceived;
        deliveryOrder.Note = dto.Note;
        var status = dto.Status;
        await UpdateChildStatus(deliveryOrder, status);
        if (session != null)
        {
            session.Status = SessionStatusEnum.Confirmed.ToString();
        }
    }
    
    public async Task UpdateFailedDelivery(DeliveryOrder deliveryOrder, DeliverySession? session,  DeliveringFailedDto dto)
    {
        deliveryOrder.Evidence = dto.Evidence;
        deliveryOrder.Reason = dto.Reason;
        deliveryOrder.Note = dto.Note;
        var status = dto.Status;
        await UpdateChildStatus(deliveryOrder, status);
        if (session != null)
        {
            session.Status = SessionStatusEnum.HandOverFailed.ToString();
        }
    }

    public async Task<DeliveringDeliveryOrderDto> GetOneDO(string code)
    {
        var deliveryOrder = _deliveryOrderRepositories
            .GetAll()
            .Include(e => e.DeliveryOrderLines)
            .FirstOrDefault(e => e.Code == code);
        if (deliveryOrder == null)
        {
            throw new DeliveryOrderNotFoundException();
        }

        var result = _mapper.Map<DeliveryOrder, DeliveringDeliveryOrderDto>(deliveryOrder);
        
        return result;
    }

    public async Task<DeliveringDOViewModel> GetAllDO(DeliveringDOQuery query)
    {
        // TODO: Add user ID
        var deliveryOrders = _deliveryOrderRepositories
                .GetAllDeliveryOrdersForDelivering(query)
                .Include(e => e.Driver)
                .Include(e => e.DeliveryOrderLines)
                .Where(e => e.ParentCode != null);

        var deliveryOrderDtos = _mapper.Map<List<DeliveryOrder>, List<DeliveringDeliveryOrderDto>>(deliveryOrders.ToList()).AsQueryable();

        var pagnition = _commonServices.CreatePaginationResponse<DeliveringDeliveryOrderDto>(deliveryOrderDtos, query);
        
        var result = new DeliveringDOViewModel()
        {
            Items = pagnition.Data.Items,
            CountPerPage = pagnition.Data.CountPerPage,
            TotalCount = pagnition.Data.TotalCount,
            TotalPage = pagnition.Data.TotalCount,
            Summary = new DOSummary()
            {
                Complete = 0,
                UnComplete = 0,
            }
        };
        
        return result;
    }
    
    public async Task<DeliveringDeliverySessionDto> GetOneSession(string code)
    {
        var deliverySession = _deliverySessionRepositories
            .GetAll()
            .Include(e => e.Coordinator)
            .Include(e => e.StartStation)
            .Include(e => e.EndStation)
            .Include(e => e.Vehicle)
            .ThenInclude(e => e.VehicleType)
            .Include(e => e.DeliveryOrders.Where(e => e.ParentCode != null))
            .ThenInclude(e => e.DeliveryOrderLines)
            .AsNoTracking()
            .FirstOrDefault(e => e.Code == code);
        
        if (deliverySession == null)
        {
            throw new DeliverySessionNotFoundException();
        }

        var deliverySessionDto = _mapper.Map<DeliverySession, DeliveringDeliverySessionDto>(deliverySession);

        return deliverySessionDto;
    }

    public async Task<DeliveringSessionViewModel> GetAllSession(DeliveringSessionQuery query)
    {
        var sessions = _deliverySessionRepositories
            .GetAllDeliverySessionsForDelivering(query);

        var sessionDtos = _mapper.Map<List<DeliverySession>, List<DeliveringDeliverySessionDto>>(sessions.ToList());
        
        var pagination = _commonServices.CreatePaginationResponse<DeliveringDeliverySessionDto>(sessionDtos.AsQueryable(), query);

        var totalDOs = sessionDtos.Sum(e => e.TotalDOs);
        
        var result = new DeliveringSessionViewModel()
        {
            Items = pagination.Data.Items,
            CountPerPage = pagination.Data.CountPerPage,
            TotalCount = pagination.Data.TotalCount,
            TotalPage = pagination.Data.TotalCount,
            Summary = new SessionSummary()
            {
                TotalSessions = pagination.Data.TotalCount,
                TotalDOs = totalDOs,
            }
        };
        
        return result;
    }

    public async Task ConfirmSession(DeliverySession session, SessionActionDto dto)
    {
        session.Evidence = dto.Evidence;
        session.Note = dto.Note;
        session.Status = dto.Status;
        // Create a new dropoff session when complete the pickup session
        if (session.SessionType == SessionTypeEnum.Pickup.ToString())
        {
            await _deliverySessionServices.CreateDropOffSession(session);
            await UpdateDeliveryOrderStatus(session.DeliveryOrders, DeliveryOrderStatusEnum.Picked.ToString());
        }
        else
        {
            await UpdateDeliveryOrderStatus(session.DeliveryOrders, DeliveryOrderStatusEnum.Picked.ToString());
        }
    }
    
    public async Task HandoverSession(DeliverySession session, SessionActionDto dto)
    {
        session.Evidence = dto.Evidence;
        session.Note = dto.Note;
        session.Status = dto.Status;
    }

    public async Task<DeliveringDeliverySessionDto> UpdateSessionStatus(string code, SessionActionDto dto)
    {
        await _unitOfWork.BeginTransactionAsync();
        var session = _deliverySessionRepositories
            .GetAll()
            .Include(e => e.Coordinator)
            .Include(e => e.StartStation)
            .Include(e => e.EndStation)
            .Include(e => e.Vehicle)
            .ThenInclude(e => e.VehicleType)
            .Include(e => e.DeliverySessionLines)
            .Include(e => e.DeliveryOrders)
            .ThenInclude(e => e.DeliveryOrderLines)
            .AsNoTracking()
            .FirstOrDefault(e => e.Code == code);
        
        if (dto.Status == SessionStatusEnum.Confirmed.ToString())
        {
            await ConfirmSession(session, dto);
        } 
        else if (dto.Status == SessionStatusEnum.HandedOver.ToString())
        {
            await HandoverSession(session, dto);
        }
        else
        {
            throw new DeliverySessionStatusNotFoundException();
        }
        
        var deliveryOrders = session.DeliveryOrders.ToList();

        session.DeliveryOrders.Clear();
        session.DeliverySessionLines.Clear();

        _deliveryOrderRepositories.UpdateRange(deliveryOrders);
        _deliverySessionRepositories.Update(session);

        await _unitOfWork.SaveChangesAsync();
        await _unitOfWork.CommitTransactionAsync();
        
        session.DeliveryOrders = session.DeliveryOrders.Where(e => e.ParentCode != null).ToList();
        var result = _mapper.Map<DeliverySession, DeliveringDeliverySessionDto>(session);
        
       
        return result;
    }
}