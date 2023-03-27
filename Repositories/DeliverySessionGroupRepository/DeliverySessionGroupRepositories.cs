using AutoMapper;
using Databases;
using Databases.Entities;
using Services.Models.DeliverySessionGroup;
using Microsoft.EntityFrameworkCore;

namespace Repositories.DeliverySessionGroupRepository;

public class DeliverySessionGroupRepositories : Repository<DeliverySessionGroup, string>, IDeliverySessionGroupRepositories
{
    private readonly IMapper _mapper;

    public DeliverySessionGroupRepositories(
        ApplicationDbContext dbContext, 
        IDateTimeProvider dateTimeProvider,
        IMapper mapper
        ) : base(dbContext, dateTimeProvider)
    {
        _mapper = mapper;
    }

    public void Create(DeliverySessionGroup deliverSessionGroup)
    {
        deliverSessionGroup.Code = "DSG" + Guid.NewGuid().ToString("n").Substring(0, 8);
        Add(deliverSessionGroup);
        UnitOfWork.SaveChanges();
    }

    public DeliverySessionGroup Init()
    {
        var groupDto = new DeliverySessionGroupDto();
        groupDto.RandomSessionGroupCode();
        var group = _mapper.Map<DeliverySessionGroupDto, DeliverySessionGroup>(groupDto);
        Add(group);
        return group;
    }
}