using Databases;
using Databases.Entities;
using Services.Models.Employee;
using Microsoft.EntityFrameworkCore;

namespace Repositories.EmployeeRepository;

public class EmployeeRepositories : Repository<Employee, string>, IEmployeeRepositories
{
    public EmployeeRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public Employee Create(Employee driver)
    {
        driver.Code = "C" + Guid.NewGuid().ToString("n").Substring(0, 8);
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

        //query = queryData.QueryByCreatedAt<Employee>(query);

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
        return GetAll().Include(e => e.Address).FirstOrDefault(e => e.Code == code);
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