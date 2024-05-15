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
    public class AngajatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AngajatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Angajats
        public async Task<IActionResult> Index()
        {
            return View(await _context.Angajat.ToListAsync());
        }

        // GET: Angajats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var angajat = await _context.Angajat
                .FirstOrDefaultAsync(m => m.AngajatId == id);
            if (angajat == null)
            {
                return NotFound();
            }

            return View(angajat);
        }

        // GET: Angajats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Angajats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AngajatId,Nume,Prenume,Telefon,Email,Functie,Salariu,Bonus")] Angajat angajat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(angajat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(angajat);
        }

        // GET: Angajats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var angajat = await _context.Angajat.FindAsync(id);
            if (angajat == null)
            {
                return NotFound();
            }
            return View(angajat);
        }

        // POST: Angajats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AngajatId,Nume,Prenume,Telefon,Email,Functie,Salariu,Bonus")] Angajat angajat)
        {
            if (id != angajat.AngajatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(angajat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AngajatExists(angajat.AngajatId))
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
            return View(angajat);
        }

        // GET: Angajats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var angajat = await _context.Angajat
                .FirstOrDefaultAsync(m => m.AngajatId == id);
            if (angajat == null)
            {
                return NotFound();
            }

            return View(angajat);
        }

        // POST: Angajats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var angajat = await _context.Angajat.FindAsync(id);
            if (angajat != null)
            {
                _context.Angajat.Remove(angajat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AngajatExists(int id)
        {
            return _context.Angajat.Any(e => e.AngajatId == id);
        }
    }
}
