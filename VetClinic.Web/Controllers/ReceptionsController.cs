using Microsoft.AspNetCore.Authorization;
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
            Employee? user = await _userManager.GetUserAsync(HttpContext.User);

            if (user != null)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains("Врач"))
                    return View(await _context.Receptions
                        .Where(r => r.EmployeeId == user.Id)
                        .ToListAsync());
            }
            
            return View(await _context.Receptions.Include(r => r.Employee).ToListAsync());
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

        public IActionResult DescribeProblem()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DescribeProblem([Bind("Description")] Reception reception)
        {
            TempData["Description"] = reception.Description;
            return RedirectToAction(nameof(ChooseDoctor));
        }

        public async Task<IActionResult> ChooseDoctor()
        {
            return View(await _userManager.GetUsersInRoleAsync("Врач"));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChooseDoctor(string id)
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

        public async Task<IActionResult> Create()
        {
            Reception reception = new Reception
            {
                TreatmentState = Reception.State.Waiting,
                Description = TempData["Description"].ToString(),
                OwnerId = (int)TempData["OwnerId"],
                AnimalId = (int)TempData["AnimalId"],
                EmployeeId = TempData["EmployeeId"].ToString(),
                Date = (DateTime)TempData["DateTime"],
            };

            reception.Owner = await _context.Owners.FindAsync(reception.OwnerId);
            reception.Animal = await _context.Animals.FindAsync(reception.AnimalId);
            reception.Employee = await _userManager.FindByIdAsync(reception.EmployeeId);

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

        #region Discharge
        public async Task<IActionResult> CheckTreatmentState(int receptionId)
        {
            Reception? reception = await _context.Receptions.FindAsync(receptionId);

            if (reception == null)
                return NotFound();

            TempData.Put("Reception", reception);

            switch (reception.TreatmentState)
            {
                case Reception.State.Waiting:
                    return RedirectToAction(nameof(ChooseDiagnoses));

                case Reception.State.InHospital:
                    return RedirectToAction(nameof(DischargeFromHospital));

                case Reception.State.TreatmentCompleted:
                    return RedirectToAction(nameof(TreatmentCompleted));
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChooseDiagnoses(int receptionId)
        {
            TempData["ReceptionId"] = receptionId;
            return View(await _context.Diagnoses.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseDiagnoses(List<int> diagnosesIds)
        {
            int receptionId = (int)TempData["ReceptionId"];
            Reception? reception = await _context.Receptions.FindAsync(receptionId);

            if (reception == null)
                return NotFound();

            reception.Diagnoses = await _context.Diagnoses
                .Where(d => diagnosesIds.Contains(d.Id))
                .ToListAsync();

            TempData.Put("Reception", reception);
            return RedirectToAction(nameof(ChooseDrugs));
        }

        public async Task<IActionResult> ChooseDrugs()
        {
            return View(await _context.Drugs.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseDrugs(List<int> drugIds)
        {
            Reception? reception = TempData.Get<Reception>("Reception");

            if (reception == null)
                return NotFound();

            reception.Drugs = await _context.Drugs
                .Where(d => drugIds.Contains(d.Id))
                .ToListAsync();

            TempData.Put("Reception", reception);
            return RedirectToAction(nameof(ChooseServices));
        }

        public async Task<IActionResult> ChooseServices()
        {
            return View(await _context.Drugs.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseServices(List<int> serviceIds)
        {
            Reception? reception = TempData.Get<Reception>("Reception");

            if (reception == null)
                return NotFound();

            reception.Services = await _context.Services
                .Where(s => serviceIds.Contains(s.Id))
                .ToListAsync();

            TempData.Put("Reception", reception);
            return RedirectToAction(nameof(ChooseTreatmentState));
        }

        public IActionResult ChooseTreatmentState()
        {
            Reception? reception = TempData.Get<Reception>("Reception");

            if (reception == null)
                return NotFound();

            return View(reception);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseTreatmentState([Bind("TreatmentState")] Reception reception)
        {
            if (reception == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(reception);

            if (reception.TreatmentState == Reception.State.InHospital)
            {
                TempData.Put("Reception", reception);
                return RedirectToAction(nameof(ChooseHospital));
            }

            _context.Receptions.Update(reception);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ChooseHospital()
        {
            return View(await _context.Hospitals.ToListAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChooseHospital(int? hospitalId)
        {
            if (hospitalId == null)
                return NotFound();

            Hospital? hospital = await _context.Hospitals.FindAsync(hospitalId);

            if (hospital == null)
                return NotFound();

            Reception? reception = TempData.Get<Reception>("Reception");

            if (reception == null)
                return NotFound();

            AnimalHospitalInfo animalHospitalInfo = new AnimalHospitalInfo
            { 
                HospitalId = hospital.Id,
                AnimalId = reception.AnimalId,
                StartDate = DateTime.Today
            };

            _context.Receptions.Update(reception);
            await _context.AnimalHospitalInfos.AddAsync(animalHospitalInfo);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> DischargeFromHospital()
        {
            Reception? reception = TempData.Get<Reception>("Reception");

            if (reception == null)
                return NotFound();

            reception.TreatmentState = Reception.State.TreatmentCompleted;
            _context.Update(reception);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult TreatmentCompleted()
        {
            return View();
        }
        #endregion

        private bool ReceptionExists(int id)
        {
            return (_context.Receptions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
