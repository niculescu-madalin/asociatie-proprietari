using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asociatie_proprietari.Data;
using asociatie_proprietari.Models;

namespace asociatie_proprietari.Controllers
{
    public class ApartamentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartamentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Apartaments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Apartament.ToListAsync());
        }

        public IActionResult ByUser(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username cannot be empty.");
            }

            var propietar = _context.Propietar
                .Include(p => p.ApartamentPropietars)
                .ThenInclude(ap => ap.Apartament)
                .FirstOrDefault(p => p.UserName == username);

            if (propietar == null)
            {
                return NotFound($"Propietar with username '{username}' not found.");
            }

            var apartments = propietar.ApartamentPropietars
                .Select(ap => ap.Apartament)
                .ToList();

            return View(apartments);
        }

        // GET: Apartaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartament = await _context.Apartament
                .FirstOrDefaultAsync(m => m.ApartamentId == id);
            if (apartament == null)
            {
                return NotFound();
            }

            return View(apartament);
        }

        // GET: Apartaments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Apartaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartamentId,NumarApartament,Scara,NumarCamere,NumarPersoane")] Apartament apartament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apartament);
        }

        // GET: Apartaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartament = await _context.Apartament.FindAsync(id);
            if (apartament == null)
            {
                return NotFound();
            }
            return View(apartament);
        }

        // POST: Apartaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApartamentId,NumarApartament,Scara,NumarCamere,NumarPersoane")] Apartament apartament)
        {
            if (id != apartament.ApartamentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartamentExists(apartament.ApartamentId))
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
            return View(apartament);
        }

        // GET: Apartaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartament = await _context.Apartament
                .FirstOrDefaultAsync(m => m.ApartamentId == id);
            if (apartament == null)
            {
                return NotFound();
            }

            return View(apartament);
        }

        // POST: Apartaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartament = await _context.Apartament.FindAsync(id);
            if (apartament != null)
            {
                _context.Apartament.Remove(apartament);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartamentExists(int id)
        {
            return _context.Apartament.Any(e => e.ApartamentId == id);
        }
    }
}
