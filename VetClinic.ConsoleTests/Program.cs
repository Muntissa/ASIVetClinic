using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VetClinic.Common;


var services = new ServiceCollection();

services.AddDbContext<VetClinicContext>(options => 
{
    options.UseSqlServer(Environment.GetEnvironmentVariable("VET_CLINIC_CONNECTION_STRING"));
});

var serviceProvider = services.BuildServiceProvider();
VetClinicContext context = serviceProvider.GetService<VetClinicContext>();

context.Hospitals.Add(new() 
{ 
    InventoryNumber = 1,
    Type = "test",
    Lenght = 100,
    Width = 100,
    Height = 100
});

context.SaveChanges();

Console.WriteLine(context.Hospitals.First().Type);