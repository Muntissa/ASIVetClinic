using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using VetClinic.Common.Entities;

namespace VetClinic.Common
{
    public class VetClinicContext : IdentityDbContext<Owner>
    {

    }
}