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
    public class PlatasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlatasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Platas
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Plata.Include(p => p.Factura);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Platas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plata = await _context.Plata
                .Include(p => p.Factura)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plata == null)
            {
                return NotFound();
            }

            return View(plata);
        }

        // GET: Platas/Create
        public IActionResult Create()
        {
            ViewData["FacturaId"] = new SelectList(_context.Factura, "Id", "Id");
            return View();
        }

        // POST: Platas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NumarCard,NumeCard,CardCVV,SumaPlatita,Status,Data,FacturaId")] Plata plata)
        {
            if (ModelState.IsValid)
            {
                _context.Add(plata);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacturaId"] = new SelectList(_context.Factura, "Id", "Id", plata.FacturaId);
            return View(plata);
        }

        // GET: Platas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plata = await _context.Plata.FindAsync(id);
            if (plata == null)
            {
                return NotFound();
            }
            ViewData["FacturaId"] = new SelectList(_context.Factura, "Id", "Id", plata.FacturaId);
            return View(plata);
        }

        // POST: Platas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NumarCard,NumeCard,CardCVV,SumaPlatita,Status,Data,FacturaId")] Plata plata)
        {
            if (id != plata.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(plata);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlataExists(plata.Id))
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
            ViewData["FacturaId"] = new SelectList(_context.Factura, "Id", "Id", plata.FacturaId);
            return View(plata);
        }

        // GET: Platas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plata = await _context.Plata
                .Include(p => p.Factura)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plata == null)
            {
                return NotFound();
            }

            return View(plata);
        }

        // POST: Platas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var plata = await _context.Plata.FindAsync(id);
            if (plata != null)
            {
                _context.Plata.Remove(plata);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlataExists(int id)
        {
            return _context.Plata.Any(e => e.Id == id);
        }
    }
}
