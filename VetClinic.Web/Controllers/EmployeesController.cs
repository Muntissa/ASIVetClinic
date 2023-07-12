using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Common.Entities;
using VetClinic.Web.Services;

namespace VetClinic.Web.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly UserManager<Employee> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserStore<Employee> _userStore;
        private readonly IUserEmailStore<Employee> _emailStore;
        private readonly IEmailSender _emailSender;

        public EmployeesController(
            UserManager<Employee> userManager,
            RoleManager<IdentityRole> roleManager,
            IUserStore<Employee> userStore,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userStore = userStore;
            _emailStore = (IUserEmailStore<Employee>)userStore;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _userManager.Users.ToListAsync());
        }

        public IActionResult RegisterNew(string role)
        {
            ViewBag.Role = role;
            return View();
        }

        [HttpPost]
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
    }
}
