using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetClinic.Common;
using VetClinic.Common.Entities;

namespace VetClinic.Web.Controllers
{
    public class ReceptionsController : Controller
    {
        private readonly VetClinicContext _context;
        private readonly UserManager<Employee> _userManager;

        public ReceptionsController(VetClinicContext context, UserManager<Employee> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Receptions != null ? 
                View(await _context.Receptions.ToListAsync()) :
                Problem("Entity set 'VetClinicContext.Receptions'  is null.");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Receptions == null)
            {
                return NotFound();
            }

            var reception = await _context.Receptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reception == null)
            {
                return NotFound();
            }

            return View(reception);
        }

        #region Appointment
        public async Task<IActionResult> ChooseOwner()
        {
            return View(await _context.Owners.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChooseOwner(int id)
        {
            TempData["OwnerId"] = id;
            return RedirectToAction(nameof(ChooseAnimal));
        }

        public async Task<IActionResult> ChooseAnimal()
        {
            return View(await _context.Animals.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChooseAnimal(int id)
        {
            TempData["AnimalId"] = id;
            return RedirectToAction(nameof(DescribeProblem));
        }

        public async Task<IActionResult> DescribeProblem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DescribeProblem([Bind("Type, Description")] Reception reception)
        {
            TempData["Type"] = reception.Type;
            TempData["Description"] = reception.Description;
            return RedirectToAction(nameof(ChooseDoctor));
        }

        public async Task<IActionResult> ChooseDoctor()
        {
            return View(await _context.Owners.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChooseDoctor(int id)
        {
            TempData["EmployeeId"] = id;
            return RedirectToAction(nameof(ChooseDateTime));
        }

        public IActionResult ChooseDateTime()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChooseDateTime([Bind("Date")] Reception reception)
        {
            TempData["DateTime"] = reception.Date;
            return RedirectToAction(nameof(Create));
        }

        public IActionResult Create()
        {
            Reception reception = new Reception
            {
                Type = TempData["Type"].ToString(),
                Description = TempData["Description"].ToString(),
                OwnerId = (int)TempData["OwnerId"],
                AnimalId = (int)TempData["AnimalId"],
                EmployeeId = (int)TempData["EmployeeId"],
                Date = (DateTime)TempData["DateTime"],
            };

            return View(reception);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reception reception)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reception);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(reception);
        }
        #endregion

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Receptions == null)
            {
                return NotFound();
            }

            var reception = await _context.Receptions.FindAsync(id);
            if (reception == null)
            {
                return NotFound();
            }
            return View(reception);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Description,Date,Price,EmployeeId")] Reception reception)
        {
            if (id != reception.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reception);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReceptionExists(reception.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reception);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Receptions == null)
            {
                return NotFound();
            }

            var reception = await _context.Receptions
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reception == null)
            {
                return NotFound();
            }

            return View(reception);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Receptions == null)
            {
                return Problem("Entity set 'VetClinicContext.Receptions'  is null.");
            }
            var reception = await _context.Receptions.FindAsync(id);
            if (reception != null)
            {
                _context.Receptions.Remove(reception);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReceptionExists(int id)
        {
          return (_context.Receptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
