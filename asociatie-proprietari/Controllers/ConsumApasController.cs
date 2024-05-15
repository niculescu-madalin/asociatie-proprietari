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
    public class ConsumApasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConsumApasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConsumApas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ConsumApa.Include(c => c.Apartament);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ConsumApas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumApa = await _context.ConsumApa
                .Include(c => c.Apartament)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumApa == null)
            {
                return NotFound();
            }

            return View(consumApa);
        }

        // GET: ConsumApas/Create
        public IActionResult Create()
        {
            ViewData["ApartamentId"] = new SelectList(_context.Apartament, "ApartamentId", "ApartamentId");
            return View();
        }

        // POST: ConsumApas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ConsumApaRece,ConsumApaCalda,ApartamentId")] ConsumApa consumApa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consumApa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartamentId"] = new SelectList(_context.Apartament, "ApartamentId", "ApartamentId", consumApa.ApartamentId);
            return View(consumApa);
        }

        // GET: ConsumApas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumApa = await _context.ConsumApa.FindAsync(id);
            if (consumApa == null)
            {
                return NotFound();
            }
            ViewData["ApartamentId"] = new SelectList(_context.Apartament, "ApartamentId", "ApartamentId", consumApa.ApartamentId);
            return View(consumApa);
        }

        // POST: ConsumApas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ConsumApaRece,ConsumApaCalda,ApartamentId")] ConsumApa consumApa)
        {
            if (id != consumApa.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consumApa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsumApaExists(consumApa.Id))
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
            ViewData["ApartamentId"] = new SelectList(_context.Apartament, "ApartamentId", "ApartamentId", consumApa.ApartamentId);
            return View(consumApa);
        }

        // GET: ConsumApas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consumApa = await _context.ConsumApa
                .Include(c => c.Apartament)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consumApa == null)
            {
                return NotFound();
            }

            return View(consumApa);
        }

        // POST: ConsumApas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consumApa = await _context.ConsumApa.FindAsync(id);
            if (consumApa != null)
            {
                _context.ConsumApa.Remove(consumApa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsumApaExists(int id)
        {
            return _context.ConsumApa.Any(e => e.Id == id);
        }
    }
}
