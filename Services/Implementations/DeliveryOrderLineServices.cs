using AutoMapper;
using Databases.Entities;
using Databases.Interfaces;
using Repositories.DeliveryOrderLineRepository;
using Repositories.DeliveryOrderRepository;
using Repositories.DeliveryPackageGroupRepository;
using Repositories.DeliveryPackageRepository;
using Services.Interfaces;
using Services.Models.DeliveryOrder;
using Services.Models.DeliveryOrderLine;
using Services.Models.DeliveryPackage;

namespace Services.Implementations;

public class DeliveryOrderLineServices : IDeliveryOrderLineServices
{
    private readonly ICommonServices _commonServices;

    private readonly IDeliveryOrderRepositories _deliveryOrderRepositories;
    private readonly IDeliveryPackageRepositories _deliveryPackageRepositories;
    private readonly IDeliveryPackageGroupRepositories _deliveryPackageGroupRepositories;
    private readonly IDeliveryOrderLineRepositories _deliveryOrderLineRepositories;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeliveryOrderLineServices(
        ICommonServices commonServices,
        IDeliveryOrderRepositories deliveryOrderRepositories,
        IDeliveryPackageRepositories deliveryPackageRepositories,
        IDeliveryPackageGroupRepositories deliveryPackageGroupRepositories,
        IDeliveryOrderLineRepositories deliveryOrderLineRepositories,
        IMapper mapper
        )
    {
        _commonServices = commonServices;
        _deliveryOrderRepositories = deliveryOrderRepositories;
        _unitOfWork = deliveryOrderRepositories.UnitOfWork;
        _deliveryPackageRepositories = deliveryPackageRepositories;
        _deliveryPackageGroupRepositories = deliveryPackageGroupRepositories;
        _deliveryOrderLineRepositories = deliveryOrderLineRepositories;
        _mapper = mapper;
    }

    private string RandomDPCode()
    {
        return "DP" + Guid.NewGuid().ToString("n").Substring(0, 8).ToUpper();
    }

    public DeliveryOrder Update(DeliveryOrder deliveryOrder, DeliveryOrderDto dataToUpdate)
    {
        // Delete all
        if (dataToUpdate?.DeliveryOrderLines?.Count == 0)
        {
            _deliveryOrderLineRepositories.DeleteRange(deliveryOrder.DeliveryOrderLines.ToList());
            return deliveryOrder;
        }

        // Delete partially
        var deleteItems = deliveryOrder.DeliveryOrderLines.Where(c => dataToUpdate.DeliveryOrderLines.All(x => x.Id != c.Id)).ToList();
        if (deleteItems.Count > 0)
        {
            _deliveryOrderLineRepositories.DeleteRange(deleteItems);
        }

        // Update
        var updateItems = dataToUpdate.DeliveryOrderLines.Where(c => deliveryOrder.DeliveryOrderLines.Any(x => x.Id == c.Id)).ToList();
        if (updateItems.Count > 0)
        {
            updateItems.ForEach(x => UpdateOne(deliveryOrder, x));
        }

        // Add new
        var newItems = dataToUpdate.DeliveryOrderLines.Where(x => x.Id == null).ToList();
        if (newItems.Count > 0)
        {
            newItems.ForEach(x => Create(deliveryOrder, x));
        }

        return deliveryOrder;
    }

    public DeliveryOrderLine UpdateOne(DeliveryOrder deliveryOrder, DeliveryOrderLineDto doLine)
    {
        var item = deliveryOrder.DeliveryOrderLines.Where(x => x.Id == doLine.Id).FirstOrDefault();

        item.Quantity = doLine.Quantity;
        item.Status = doLine.Status;
        item.Weight = doLine.Weight;
        item.Width = doLine.Width;
        item.Length = doLine.Length;
        item.Height = doLine.Height;

        item.DeliveryPackage.ExternalCode = doLine.ExternalCode;
        item.DeliveryPackage.Name = doLine.Name;
        item.DeliveryPackage.Uom = doLine.Uom;
        item.DeliveryPackage.Status = doLine.PackageStatus;

        _deliveryOrderLineRepositories.Update(item);

        return item;
    }

    public DeliveryOrderLine Create(DeliveryOrder deliveryOrder, DeliveryOrderLineDto doLineDto)
    {
        var doDto = _mapper.Map<DeliveryOrder, DeliveryOrderDto>(deliveryOrder);
        var doLineToCreate = InitData(doLineDto, doDto);

        var doLine = _mapper.Map<DeliveryOrderLineDto, DeliveryOrderLine>(doLineToCreate);
        _deliveryOrderLineRepositories.Add(doLine);

        return doLine;
    }

    public DeliveryOrderLineDto InitData(DeliveryOrderLineDto doLineDto, DeliveryOrderDto deliveryOrderDto)
    {
        var deliveryPackage = new DeliveryPackageDto()
        {
            Code = RandomDPCode(),
            ExternalCode = doLineDto.ExternalCode,
            Name = doLineDto.Name,
            Uom = doLineDto.Uom,
        };

        doLineDto.Id = Guid.NewGuid();
        doLineDto.Code = deliveryPackage.Code;
        doLineDto.DeliveryOrderCode = deliveryOrderDto.Code;
        doLineDto.DeliveryPackageCode = deliveryPackage.Code;
        doLineDto.DeliveryPackage = deliveryPackage;

        return doLineDto;
    }
}