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
    public class AnimalsController : Controller
    {
        private readonly VetClinicContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public AnimalsController(VetClinicContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> Index(int? ownerId)
        {
            if (ownerId == null)
                return View(await _context.Animals.ToListAsync());

            Owner? owner = await _context.Owners.FindAsync(ownerId);

            if (owner == null)
                return NotFound();

            return View(owner.Animals);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animals == null)
                return NotFound();

            var animal = await _context.Animals
                .FirstOrDefaultAsync(m => m.Id == id);

            if (animal == null)
                return NotFound();

            return View(animal);
        }

        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "DocumentData");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Sex,Breed,Color,Weight,DateOfBirth,PhotoPath")] Animal animal, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                var fileName = "";
                var filePath = "";

                if (upload != null && upload.Length > 0)
                {
                    fileName = Path.GetFileName(upload.FileName);
                    filePath = Path.Combine(_appEnvironment.WebRootPath, "files", fileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await upload.CopyToAsync(fileStream);
                    }
                    animal.PhotoPath = "/files/" + fileName;
                }

                animal.Status = "Ожидание";

                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(animal);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animals == null)
                return NotFound();

            var animal = await _context.Animals.FindAsync(id);

            if (animal == null)
                return NotFound();

            return View(animal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Sex,Breed,Color,Weight,DateOfBirth,PhotoPath")] Animal animal, IFormFile upload)
        {
            if (id != animal.Id)
                return NotFound();


            var fileName = "";
            var filePath = "";

            if (upload != null && upload.Length > 0)
            {
                fileName = Path.GetFileName(upload.FileName);
                filePath = Path.Combine(_appEnvironment.WebRootPath, "files", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await upload.CopyToAsync(fileStream);
                }
                animal.PhotoPath = "/files/" + fileName;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(animal);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animals == null)
                return NotFound();

            var animal = await _context.Animals
                .FirstOrDefaultAsync(m => m.Id == id);

            if (animal == null)
                return NotFound();

            return View(animal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animals == null)
            {
                return Problem("Entity set 'VetClinicContext.Animals'  is null.");
            }
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
          return (_context.Animals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
