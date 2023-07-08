using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinic.Common;
using VetClinic.Common.Entities;

namespace VetClinic.Web.Controllers
{
    public class AnimalHospitalInfoesController : Controller
    {
        private readonly VetClinicContext _context;

        public AnimalHospitalInfoesController(VetClinicContext context)
        {
            _context = context;
        }

        // GET: AnimalHospitalInfoes
        public async Task<IActionResult> Index()
        {
            var vetClinicContext = _context.AnimalHospitalInfos.Include(a => a.Animal).Include(a => a.Hospital);
            return View(await vetClinicContext.ToListAsync());
        }

        // GET: AnimalHospitalInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AnimalHospitalInfos == null)
            {
                return NotFound();
            }

            var animalHospitalInfo = await _context.AnimalHospitalInfos
                .Include(a => a.Animal)
                .Include(a => a.Hospital)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalHospitalInfo == null)
            {
                return NotFound();
            }

            return View(animalHospitalInfo);
        }

        // GET: AnimalHospitalInfoes/Create
        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Breed");
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Type");
            return View();
        }

        // POST: AnimalHospitalInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AnimalId,HospitalId,StartDate,EndDate")] AnimalHospitalInfo animalHospitalInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(animalHospitalInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Breed", animalHospitalInfo.AnimalId);
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Type", animalHospitalInfo.HospitalId);
            return View(animalHospitalInfo);
        }

        // GET: AnimalHospitalInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AnimalHospitalInfos == null)
            {
                return NotFound();
            }

            var animalHospitalInfo = await _context.AnimalHospitalInfos.FindAsync(id);
            if (animalHospitalInfo == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Breed", animalHospitalInfo.AnimalId);
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Type", animalHospitalInfo.HospitalId);
            return View(animalHospitalInfo);
        }

        // POST: AnimalHospitalInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnimalId,HospitalId,StartDate,EndDate")] AnimalHospitalInfo animalHospitalInfo)
        {
            if (id != animalHospitalInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animalHospitalInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalHospitalInfoExists(animalHospitalInfo.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Breed", animalHospitalInfo.AnimalId);
            ViewData["HospitalId"] = new SelectList(_context.Hospitals, "Id", "Type", animalHospitalInfo.HospitalId);
            return View(animalHospitalInfo);
        }

        // GET: AnimalHospitalInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AnimalHospitalInfos == null)
            {
                return NotFound();
            }

            var animalHospitalInfo = await _context.AnimalHospitalInfos
                .Include(a => a.Animal)
                .Include(a => a.Hospital)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animalHospitalInfo == null)
            {
                return NotFound();
            }

            return View(animalHospitalInfo);
        }

        // POST: AnimalHospitalInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AnimalHospitalInfos == null)
            {
                return Problem("Entity set 'VetClinicContext.AnimalHospitalInfos'  is null.");
            }
            var animalHospitalInfo = await _context.AnimalHospitalInfos.FindAsync(id);
            if (animalHospitalInfo != null)
            {
                _context.AnimalHospitalInfos.Remove(animalHospitalInfo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalHospitalInfoExists(int id)
        {
          return (_context.AnimalHospitalInfos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
