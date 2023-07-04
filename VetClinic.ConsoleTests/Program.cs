using Microsoft.EntityFrameworkCore;
using VetClinic.Common;
using VetClinic.Common.Entities;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

using var context = new VetClinicContext();

if (context.Database.GetPendingMigrations().Any())
    context.Database.Migrate();

context.Set<Service>().Add(new() { Name = "amksdmaks", Price = 1000 });

context.SaveChanges();

Console.WriteLine(context.Services.First().Name);

Console.ReadLine();