using Databases.Entities;
using Services.Models.DeliverySession;
using Services.Models.DeliverySessionLine;

namespace Services.Interfaces;

public interface IDeliverySessionLineServices
{
    string RandomDSLCode();
    List<DeliverySessionLine> UpdateMany(List<DeliverySessionLine> sessionLines, DeliverySessionDto datasToUpdate);
    DeliverySessionLine UpdateOne(DeliverySessionLine sessionLine, DeliverySessionLineDto dataToUpdate);
    void CreateMany(List<DeliverySessionLineDto> datasToCreate, DeliverySessionDto sessionDto);
    void CreateOne(DeliverySessionLineDto dataToCreate, DeliverySessionDto sessionDto);
    void DeleteOne(DeliverySessionLine sessionLine);
    void DeleteAllSessionLines(DeliverySession session);
}