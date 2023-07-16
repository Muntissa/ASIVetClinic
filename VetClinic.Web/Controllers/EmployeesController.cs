using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Common;
using VetClinic.Common.Entities;
using VetClinic.Web.Services;

namespace VetClinic.Web.Controllers
{
    public class EmployeesController : Controller
    {
        public enum IndexShowType
        {
            SinglePerson,
            List
        }

        private readonly VetClinicContext _context;
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<Employee> _userStore;
        private readonly IUserEmailStore<Employee> _emailStore;
        private readonly IEmailSender _emailSender;

        public EmployeesController(
            VetClinicContext context,
            UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<Employee> userStore,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<Employee>)userStore;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index(string? role = null)
        {
            if (role != null )
            {
                ViewBag.Role = role;

                return View(await _userManager.GetUsersInRoleAsync(role));
            }
                

            var admins = await _userManager.GetUsersInRoleAsync("admin");

            IList<Employee> users = await _userManager
                            .Users
                            .Where(u => !admins.Contains(u))
                            .ToListAsync();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                user.Email = roles.Count > 0 ? roles[0] : "[Роль не присвоена]";
            }

            return View(users);
        }

        public async Task<IActionResult> HeadDoctorDetails()
        {
            var heads = await _userManager.GetUsersInRoleAsync("Главный врач");

            if (heads.Count == 0)
                return NotFound();

            if (heads.Count == 1)
                return RedirectToAction(nameof(Details), new { id = heads[0].Id });

            return RedirectToAction(nameof(Index), new { role = "Главный врач" });
        }

        public async Task<IActionResult> Details(string? id)
        {
            if (id == null || _userManager.Users == null)
                return NotFound();

            Employee? employee = await _userManager.FindByIdAsync(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        public IActionResult RegisterNew(string role)
        {
            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterNew(Employee employee, string role)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Role = role;
                return View();
            }

            employee.UserName = employee.Surname + employee.Name + employee.Patronymic;
            employee.EmploymentDate = DateTime.Today;

            await _emailStore.SetEmailAsync(employee, employee.Email, CancellationToken.None);

            string password = await TempPasswordService.GetPasswordForNewEmployeeAsync(_userManager);

            var userCreationResult = await _userManager.CreateAsync(employee, password);

            if (!userCreationResult.Succeeded)
            {
                foreach (var error in userCreationResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                ViewBag.Role = role;
                return View();
            }

            await _emailSender.SendEmailAsync(employee.Email, "Измените ваш пароль",
                $"Здравствуйте, {employee.Name} {employee.Patronymic}!\n" +
                $"Ваш временный пароль для входа в систему ветклиники: {password}\n" +
                $"Пожалуйста, измените его как можно скорее в вашем личном кабинете");

            var roleAdditionResult = await _userManager.AddToRoleAsync(employee, role);

            if (!roleAdditionResult.Succeeded)
            {
                foreach (var error in roleAdditionResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                ViewBag.Role = role;
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _userManager.Users == null)
                return NotFound();

            Employee? employee = await _userManager.FindByIdAsync(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, /*[Bind("Id,Name,Price")]*/ Employee employee)
        {
            if (id != employee.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(employee);

            Employee? employeeToUpdate = await _userManager.FindByIdAsync(employee.Id);

            if (employeeToUpdate == null)
                return NotFound();

            employeeToUpdate.Surname = employee.Surname;
            employeeToUpdate.Name = employee.Name;
            employeeToUpdate.Patronymic = employee.Patronymic;
            employeeToUpdate.Sex = employee.Sex;
            employeeToUpdate.DateOfBirth = employee.DateOfBirth;
            employeeToUpdate.EmploymentDate = employee.EmploymentDate;
            employeeToUpdate.Position = employee.Position;
            employeeToUpdate.Qualification = employee.Qualification;
            employeeToUpdate.Email = employee.Email;
            employeeToUpdate.PhoneNumber = employee.PhoneNumber;

            await _userManager.UpdateAsync(employeeToUpdate);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null || _userManager.Users == null)
                return NotFound();

            Employee? employee = await _userManager.FindByIdAsync(id);

            if (employee == null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Employee? employee = await _userManager.FindByIdAsync(id);

            if (employee != null)
                await _userManager.DeleteAsync(employee);

            return RedirectToAction(nameof(Index));
        }
    }
}
