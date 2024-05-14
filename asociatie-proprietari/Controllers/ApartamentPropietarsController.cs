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
    public class ApartamentPropietarsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartamentPropietarsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ApartamentPropietars
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ApartamentPropietar.Include(a => a.Apartament).Include(a => a.Propietar);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ApartamentPropietars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamentPropietar = await _context.ApartamentPropietar
                .Include(a => a.Apartament)
                .Include(a => a.Propietar)
                .FirstOrDefaultAsync(m => m.id == id);
            if (apartamentPropietar == null)
            {
                return NotFound();
            }

            return View(apartamentPropietar);
        }

        // GET: ApartamentPropietars/Create
        public IActionResult Create()
        {
            ViewData["ApartamentId"] = new SelectList(_context.Apartament, "ApartamentId", "ApartamentId");
            ViewData["PropietarId"] = new SelectList(_context.Propietar, "Id", "Id");
            return View();
        }

        // POST: ApartamentPropietars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ApartamentId,PropietarId")] ApartamentPropietar apartamentPropietar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartamentPropietar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ApartamentId"] = new SelectList(_context.Apartament, "ApartamentId", "ApartamentId", apartamentPropietar.ApartamentId);
            ViewData["PropietarId"] = new SelectList(_context.Propietar, "Id", "Id", apartamentPropietar.PropietarId);
            return View(apartamentPropietar);
        }

        // GET: ApartamentPropietars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamentPropietar = await _context.ApartamentPropietar.FindAsync(id);
            if (apartamentPropietar == null)
            {
                return NotFound();
            }
            ViewData["ApartamentId"] = new SelectList(_context.Apartament, "ApartamentId", "ApartamentId", apartamentPropietar.ApartamentId);
            ViewData["PropietarId"] = new SelectList(_context.Propietar, "Id", "Id", apartamentPropietar.PropietarId);
            return View(apartamentPropietar);
        }

        // POST: ApartamentPropietars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ApartamentId,PropietarId")] ApartamentPropietar apartamentPropietar)
        {
            if (id != apartamentPropietar.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartamentPropietar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartamentPropietarExists(apartamentPropietar.id))
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
            ViewData["ApartamentId"] = new SelectList(_context.Apartament, "ApartamentId", "ApartamentId", apartamentPropietar.ApartamentId);
            ViewData["PropietarId"] = new SelectList(_context.Propietar, "Id", "Id", apartamentPropietar.PropietarId);
            return View(apartamentPropietar);
        }

        // GET: ApartamentPropietars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartamentPropietar = await _context.ApartamentPropietar
                .Include(a => a.Apartament)
                .Include(a => a.Propietar)
                .FirstOrDefaultAsync(m => m.id == id);
            if (apartamentPropietar == null)
            {
                return NotFound();
            }

            return View(apartamentPropietar);
        }

        // POST: ApartamentPropietars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartamentPropietar = await _context.ApartamentPropietar.FindAsync(id);
            if (apartamentPropietar != null)
            {
                _context.ApartamentPropietar.Remove(apartamentPropietar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartamentPropietarExists(int id)
        {
            return _context.ApartamentPropietar.Any(e => e.id == id);
        }
    }
}
