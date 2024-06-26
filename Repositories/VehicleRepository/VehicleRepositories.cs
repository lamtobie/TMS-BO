﻿using Databases;
using Databases.Entities;
using Services.Models.Vehicle;
using Microsoft.EntityFrameworkCore;
using Services.Models.Employee;

namespace Repositories.VehicleRepository;

public class VehicleRepositories : Repository<Vehicle, string>, IVehicleRepositories
{
    public VehicleRepositories(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider) : base(dbContext, dateTimeProvider)
    {
    }

    public IQueryable<Vehicle> GetAllVehicles(VehicleQuery queryData)
    {
        var query = GetAll();
        if (queryData.Keyword != null)
        {
            query = query.Where(e => e.NumberPlate.ToLower().Contains(queryData.Keyword.ToLower()));
        }

        if (queryData.Status != null)
        {
            query = query.Where(e => e.Status == queryData.Status);
        }

        if (queryData.VehicleTypeCode != null)
        {
            query = query.Where(e => e.VehicleTypeCode == queryData.VehicleTypeCode);
        }

        if (queryData.CreatedAt != null && queryData.GetTimeRange<EmployeeQuery>("CreatedAt").Count > 0)
        {
            var range = queryData.GetTimeRange<EmployeeQuery>("CreatedAt");
            query = query.Where(x => x.CreatedAt >= range[0] && x.CreatedAt <= range[1]);
        }

        return query.Include(e => e.VehicleType);
    }

    public Vehicle? GetVehicleByCode(string code)
    {
        var vehicle = GetAll().Include(e => e.VehicleType).FirstOrDefault(e => e.Code == code);
        return vehicle;
    }

    public Vehicle? GetBaseVehicleInformation(string code)
    {
        var vehicle = GetAll().FirstOrDefault(e => e.Code == code);
        return vehicle;
    }

    public Vehicle CreateVehicle(Vehicle vehicle)
    {
        Add(vehicle);
        return vehicle;
    }

    public Vehicle UpdateVehicle(Vehicle vehicle)
    {
        Update(vehicle);
        return vehicle;
    }

    public Vehicle? DeleteVehicle(Vehicle vehicle)
    {
        Delete(vehicle);
        return vehicle;
    }
}