using Repositories;
using Databases.Entities;


namespace Repositories.DataAttributeRepository;

public interface IDataAttributeRepositories : IRepository<DataAttribute, Guid>
{
    void Create(DataAttribute DataAttribute);
    DataAttribute DeleteDataAttribute(Guid id);
    DataAttribute UpdateDataAttribute(DataAttribute DataAttribute);
}