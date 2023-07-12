using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VetClinic.Common.Entities;

namespace VetClinic.Web.Services
{
    public static class TempPasswordService
    {
        public static async Task<string> GetPasswordForNewEmployeeAsync(UserManager<Employee> userManager)
        {
            int number = await userManager.Users.CountAsync();
            return $"employee-{number + 1}-temp";
        }
    }
}
