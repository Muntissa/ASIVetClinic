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
    public class ReceptionsController : Controller
    {
        private readonly VetClinicContext _context;

        public ReceptionsController(VetClinicContext context)
        {
            _context = context;
        }

        // GET: Receptions
        public async Task<IActionResult> Index()
        {
              return _context.Receptions != null ? 
                          View(await _context.Receptions.ToListAsync()) :
                          Problem("Entity set 'VetClinicContext.Receptions'  is null.");
        }

        // GET: Receptions/Details/5
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

        // GET: Receptions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Receptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Description,Date,Price,EmployeeId")] Reception reception)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reception);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reception);
        }

        // GET: Receptions/Edit/5
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

        // POST: Receptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Receptions/Delete/5
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

        // POST: Receptions/Delete/5
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
