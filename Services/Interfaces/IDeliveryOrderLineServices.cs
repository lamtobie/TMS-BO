using Databases.Entities;
using Services.Models.DeliveryOrder;
using Services.Models.DeliveryOrderLine;

namespace Services.Interfaces;

public interface IDeliveryOrderLineServices
{
    DeliveryOrder Update(DeliveryOrder deliveryOrder, DeliveryOrderDto dataToUpdate);
    DeliveryOrderLine UpdateOne(DeliveryOrder deliveryOrder, DeliveryOrderLineDto doLine);
    DeliveryOrderLine Create(DeliveryOrder deliveryOrder, DeliveryOrderLineDto doLine);
    DeliveryOrderLineDto InitData(DeliveryOrderLineDto doLineDto, DeliveryOrderDto deliveryOrderDto);
}