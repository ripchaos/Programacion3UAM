using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FutbolApp.Controllers
{
    public class FutbolistasController : Controller
    {
        private readonly AppDbContext _context;

        public FutbolistasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Futbolistas
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Futbolistas.Include(f => f.Club);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Futbolistas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var futbolista = await _context.Futbolistas
                .Include(f => f.Club)
                .FirstOrDefaultAsync(m => m.FutbolistaId == id);
            if (futbolista == null)
            {
                return NotFound();
            }

            return View(futbolista);
        }

        // GET: Futbolistas/Create
        public IActionResult Create()
        {
            ViewData["ClubId"] = new SelectList(_context.Clubes, "ClubId", "NombreClub");
            return View();
        }

        // POST: Futbolistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FutbolistaId,NombreCompleto,EdadActual,Rol,CamisetaNumero,ClubId")] Futbolista futbolista)
        {
            if (ModelState.IsValid)
            {
                _context.Add(futbolista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClubId"] = new SelectList(_context.Clubes, "ClubId", "NombreClub", futbolista.ClubId);
            return View(futbolista);
        }

        // GET: Futbolistas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var futbolista = await _context.Futbolistas.FindAsync(id);
            if (futbolista == null)
            {
                return NotFound();
            }
            ViewData["ClubId"] = new SelectList(_context.Clubes, "ClubId", "NombreClub", futbolista.ClubId);
            return View(futbolista);
        }

        // POST: Futbolistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FutbolistaId,NombreCompleto,EdadActual,Rol,CamisetaNumero,ClubId")] Futbolista futbolista)
        {
            if (id != futbolista.FutbolistaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(futbolista);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FutbolistaExists(futbolista.FutbolistaId))
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
            ViewData["ClubId"] = new SelectList(_context.Clubes, "ClubId", "NombreClub", futbolista.ClubId);
            return View(futbolista);
        }

        // GET: Futbolistas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var futbolista = await _context.Futbolistas
                .Include(f => f.Club)
                .FirstOrDefaultAsync(m => m.FutbolistaId == id);
            if (futbolista == null)
            {
                return NotFound();
            }

            return View(futbolista);
        }

        // POST: Futbolistas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var futbolista = await _context.Futbolistas.FindAsync(id);
            if (futbolista != null)
            {
                _context.Futbolistas.Remove(futbolista);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FutbolistaExists(int id)
        {
            return _context.Futbolistas.Any(e => e.FutbolistaId == id);
        }
    }
}
