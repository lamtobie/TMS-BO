using AutoMapper;
using Databases.Entities;
using Repositories.DeliverySessionLineRepository;
using Repositories.DeliverySessionRepository;
using Repositories.DeliveryPackageGroupRepository;
using Repositories.DeliveryPackageRepository;
using Services.Interfaces;
using Services.Models.DeliverySession;
using Services.Models.DeliverySessionLine;
using Repositories.DeliveryOrderRepository;
using Databases.Interfaces;

namespace Services.Implementations;

public class DeliverySessionLineServices : IDeliverySessionLineServices
{
    private readonly ICommonServices _commonServices;

    private readonly IDeliverySessionRepositories _deliverySessionRepositories;
    private readonly IDeliveryPackageRepositories _deliveryPackageRepositories;
    private readonly IDeliveryPackageGroupRepositories _deliveryPackageGroupRepositories;
    private readonly IDeliverySessionLineRepositories _deliverySessionLineRepositories;
    private readonly IDeliveryOrderRepositories _deliveryOrderRepositories;

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeliverySessionLineServices(
        ICommonServices commonServices,
        IDeliverySessionRepositories deliverySessionRepositories,
        IDeliveryPackageRepositories deliveryPackageRepositories,
        IDeliveryPackageGroupRepositories deliveryPackageGroupRepositories,
        IDeliverySessionLineRepositories deliverySessionLineRepositories,
        IDeliveryOrderRepositories deliveryOrderRepositories,
        IMapper mapper
        )
    {
        _commonServices = commonServices;
        _deliverySessionRepositories = deliverySessionRepositories;
        _unitOfWork = deliverySessionRepositories.UnitOfWork;
        _deliveryPackageRepositories = deliveryPackageRepositories;
        _deliveryPackageGroupRepositories = deliveryPackageGroupRepositories;
        _deliverySessionLineRepositories = deliverySessionLineRepositories;
        _deliveryOrderRepositories = deliveryOrderRepositories;
        _mapper = mapper;
    }

    public string RandomDSLCode()
    {
        return "DSL" + Guid.NewGuid().ToString("n").Substring(0, 8).ToUpper();
    }

    public List<DeliverySessionLine> UpdateMany(List<DeliverySessionLine> sessionLines, DeliverySessionDto dataToUpdate)
    {
        sessionLines.ForEach(x =>
        {
            var data = dataToUpdate.DeliverySessionLines?.Where(y => y.Code == x.Code).FirstOrDefault();
            if (data != null)
            {
                UpdateOne(x, data);
            }
        });

        return sessionLines;
    }

    public DeliverySessionLine UpdateOne(DeliverySessionLine sessionLine, DeliverySessionLineDto dataToUpdate)
    {
        sessionLine.DeliveryOrderGroupCode = dataToUpdate.DeliveryOrderGroupCode;
        sessionLine.DeliveryOrderParentCode = dataToUpdate.DeliveryOrderParentCode;
        sessionLine.DeliveryOrderChildrenCode = dataToUpdate.DeliveryOrderChildrenCode;
        sessionLine.DeliveryOrderCode = dataToUpdate.DeliveryOrderCode;
        sessionLine.ReferenceCode = dataToUpdate.ReferenceCode;
        sessionLine.DeliveryPackageGroupCode = dataToUpdate.DeliveryPackageGroupCode;
        sessionLine.DeliveryPackageCode = dataToUpdate.DeliveryPackageCode;
        sessionLine.Status = dataToUpdate.Status;
        sessionLine.ConsumedAt = dataToUpdate.ConsumedAt;
        sessionLine.ConsumedBy = dataToUpdate.ConsumedBy;

        _deliverySessionLineRepositories.Update(sessionLine);

        return sessionLine;
    }

    public void CreateMany(List<DeliverySessionLineDto> datasToCreate, DeliverySessionDto sessionDto)
    {
        datasToCreate?.ForEach(dataToCreate => CreateOne(dataToCreate, sessionDto));
    }

    public void CreateOne(DeliverySessionLineDto dataToCreate, DeliverySessionDto sessionDto)
    {
        var sessionLineDto = dataToCreate.CreateSessionLine(sessionDto);
        var newSessionLine = _mapper.Map<DeliverySessionLineDto, DeliverySessionLine>(sessionLineDto);
        _deliverySessionLineRepositories.Add(newSessionLine);
    }

    public void DeleteOne(DeliverySessionLine sessionLine)
    {
        _deliverySessionLineRepositories.Delete(sessionLine);
    }

    public void DeleteAllSessionLines(DeliverySession session)
    {
        _deliverySessionLineRepositories.DeleteRange(session.DeliverySessionLines.ToList());
    }
}