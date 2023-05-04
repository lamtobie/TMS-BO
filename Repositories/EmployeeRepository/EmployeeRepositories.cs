using Databases;
using Databases.Entities;
using Services.Models.Employee;
using Microsoft.EntityFrameworkCore;
using Services.Models.DeliveryOrder;

namespace Repositories.EmployeeRepository;

public class EmployeeRepositories : Repository<Employee, string>, IEmployeeRepositories
{
    public EmployeeRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public Employee Create(Employee driver)
    {
        Add(driver);
        UnitOfWork.SaveChanges();
        return driver;
    }

    public IQueryable<Employee> GetAllEmployees(EmployeeQuery queryData)
    {
        IQueryable<Employee> query = GetAll();

        if (queryData.Keyword != null)
        {
            query = query.Where(q => q.Code.ToLower() == queryData.Keyword.ToLower() ||
                                    q.MobilePhone == queryData.Keyword ||
                                    q.StationCode == queryData.Keyword ||
                                    q.FullName.ToLower().Contains(queryData.Keyword.ToLower()));
        }

        if (queryData.Status != null)
        {
            query = query.Where(q => q.Status == queryData.Status);
        }

        if (queryData.EmployeeType != null)
        {
            query = query.Where(q => q.EmployeeType == queryData.EmployeeType);
        }

        if (queryData.CreatedAt != null && queryData.GetTimeRange<EmployeeQuery>("CreatedAt").Count > 0)
        {
            var range = queryData.GetTimeRange<EmployeeQuery>("CreatedAt");
            query = query.Where(x => x.CreatedAt >= range[0] && x.CreatedAt <= range[1]);
        }

        return query;
    }
    public IQueryable<Employee> GetAllDrivers(EmployeeQuery queryData)
    {
        IQueryable<Employee> query = GetAll().Where(q => q.EmployeeType.Equals("driver"));

        if (queryData.Keyword != null)
        {
            query = query.Where(q => q.Code.ToLower() == queryData.Keyword.ToLower() ||
                                    q.MobilePhone == queryData.Keyword ||
                                    q.StationCode == queryData.Keyword ||
                                    q.FullName.ToLower().Contains(queryData.Keyword.ToLower()));
        }

        if (queryData.Status != null)
        {
            query = query.Where(q => q.Status == queryData.Status);
        }

        if (queryData.EmployeeType != null)
        {
            query = query.Where(q => q.EmployeeType == queryData.EmployeeType);
        }

        if (queryData.CreatedAt != null && queryData.GetTimeRange<EmployeeQuery>("CreatedAt").Count > 0)
        {
            var range = queryData.GetTimeRange<EmployeeQuery>("CreatedAt");
            query = query.Where(x => x.CreatedAt >= range[0] && x.CreatedAt <= range[1]);
        }

        return query;
    }

    public IQueryable<Employee> GetAllCoordinators(EmployeeQuery queryData)
    {
        IQueryable<Employee> query = GetAll().Where(q => q.EmployeeType.Equals("coordinator"));

        if (queryData.Keyword != null)
        {
            query = query.Where(q => q.Code.ToLower() == queryData.Keyword.ToLower() ||
                                    q.MobilePhone == queryData.Keyword ||
                                    q.StationCode == queryData.Keyword ||
                                    q.FullName.ToLower().Contains(queryData.Keyword.ToLower()));
        }

        if (queryData.Status != null)
        {
            query = query.Where(q => q.Status == queryData.Status);
        }

        if (queryData.EmployeeType != null)
        {
            query = query.Where(q => q.EmployeeType == queryData.EmployeeType);
        }

        if (queryData.CreatedAt != null && queryData.GetTimeRange<EmployeeQuery>("CreatedAt").Count > 0)
        {
            var range = queryData.GetTimeRange<EmployeeQuery>("CreatedAt");
            query = query.Where(x => x.CreatedAt >= range[0] && x.CreatedAt <= range[1]);
        }

        return query;
    }

    public Employee DeleteEmployee(string code)
    {
        var driver = GetAll().First(e => e.Code == code);
        Delete(driver);
        UnitOfWork.SaveChanges();
        return driver;
    }

    public Employee UpdateEmployee(Employee driver)
    {
        Update(driver);
        UnitOfWork.SaveChanges();
        return driver;
    }

    public Employee? GetEmployeeByCode(string code)
    {
        return GetAll().Include(e => e.Station).Include(e => e.Address).FirstOrDefault(e => e.Code == code);
    }

    public Employee? GetEmployeeByPhone(string phone)
    {
        return GetAll().FirstOrDefault(e => e.MobilePhone == phone);
    }

    public Employee? GetEmployeeByEmail(string email)
    {
        return GetAll().FirstOrDefault(e => e.Email == email);
    }

    public Employee? GetEmployeeByPhoneAndEmail(string phone, string email)
    {
        return GetAll().FirstOrDefault(e => e.MobilePhone == phone && e.Email == email);
    }
}