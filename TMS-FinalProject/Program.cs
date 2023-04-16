using Databases;
using Services;
using Mapper.Installer;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Repositories;
using Repositories.AddressRepository;
using Repositories.DataAttributeRepository;
using Repositories.DeliveryOrderGroupRepository;
using Repositories.DeliveryOrderLineRepository;
using Repositories.DeliveryOrderRepository;
using Repositories.DeliveryPackageGroupRepository;
using Repositories.DeliveryPackageRepository;
using Repositories.DeliveryRouteRepository;
using Repositories.DeliveryRouteSegmentRepository;
using Repositories.DeliverySessionGroupRepository;
using Repositories.DeliverySessionLineRepository;
using Repositories.DeliverySessionRepository;
using Repositories.EmployeeRepository;
using Repositories.StationRepository;
using Repositories.VehicleRepository;
using Repositories.VehicleTypeRepository;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMvc().AddJsonOptions(options =>
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase);
builder.Services.AddNpgsql("Server=localhost;Port=5432;Database=tms;Username=postgres;Password=123;","TMS-FinalProject");
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDateTimeProvider();
builder.Services.AddServices();
builder.Services.AddMapper();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x
       .AllowAnyMethod()
       .AllowAnyHeader()
       .SetIsOriginAllowed(origin => true) // allow any origin
       .AllowCredentials()); // allow credentials
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseAuthorization();

app.MapControllers();

app.Run();
