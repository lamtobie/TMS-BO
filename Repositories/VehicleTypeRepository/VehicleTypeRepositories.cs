using Databases;
using Databases.Entities;
using Services.Models.VehicleType;
using Microsoft.EntityFrameworkCore;
using Services.Models.Employee;

namespace Repositories.VehicleTypeRepository
{
    public class VehicleTypeRepositories : Repository<VehicleType, string>, IVehicleTypeRepositories
    {
        public VehicleTypeRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
        {
        }

        public void CreateVehicleType(VehicleType vehicleType)
        {
            Add(vehicleType);
            UnitOfWork.SaveChanges();
        }

        public VehicleType DeleteVehicleType(string code)
        {
            var data = GetAll().First(e => e.Code == code);
            Delete(data);
            UnitOfWork.SaveChanges();
            return data;
        }

        public IQueryable<VehicleType> GetAllVehicleType(VehicleTypeQuery queryData)
        {
            IQueryable<VehicleType> query = GetAll();

            if (queryData.Keyword != null)
            {
                var pattern = $"%{queryData.Keyword}%";
                query = query.Where(q => EF.Functions.Like(q.Code, pattern) ||
                                    EF.Functions.Like(q.Name, pattern));
            }

            if (queryData.Status != null)
            {
                query = query.Where(e => e.Status == queryData.Status);
            }

            if (queryData.CreatedAt != null && queryData.GetTimeRange<VehicleTypeQuery>("CreatedAt").Count > 0)
            {
                var range = queryData.GetTimeRange<VehicleTypeQuery>("CreatedAt");
                query = query.Where(x => x.CreatedAt >= range[0] && x.CreatedAt <= range[1]);
            }

            return query.Include(e => e.Vehicles);
        }

        public VehicleType? GetVehicleTypeByCode(string code)
        {
            return GetAll().Include(e => e.Vehicles).FirstOrDefault(e => e.Code == code);
        }

        public VehicleType? GetVehicleTypeByName(string name)
        {
            return GetAll().FirstOrDefault(e => e.Name == name);
        }

        public VehicleType? GetVehicleTypeBaseInformation(string code)
        {
            return GetAll().FirstOrDefault(e => e.Code == code);
        }

        public VehicleType UpdateVehicleType(VehicleType vehicleType)
        {
            Update(vehicleType);
            UnitOfWork.SaveChanges();
            return vehicleType;
        }
    }
}
