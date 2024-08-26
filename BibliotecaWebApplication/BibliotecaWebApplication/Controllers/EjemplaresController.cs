using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BibliotecaWebApplication.Data;
using BibliotecaWebApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaWebApplication.Controllers
{
    public class EjemplaresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EjemplaresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Ejemplares.Include(e => e.Estante).Include(e => e.Publicacion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ejemplares/Details/5
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ejemplar = await _context.Ejemplares
                .Include(e => e.Estante)
                .Include(e => e.Publicacion)
                .FirstOrDefaultAsync(m => m.EjemplarId == id);
            if (ejemplar == null)
            {
                return NotFound();
            }

            return View(ejemplar);
        }

        // GET: Ejemplares/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewData["EstanteId"] = new SelectList(_context.Estantes, "EstanteId", "EstanteId");
            ViewData["PublicacionId"] = new SelectList(_context.Publicaciones, "PublicacionId", "PublicacionId");
            return View();
        }

        // POST: Ejemplares/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("EjemplarId,PublicacionId,EstanteId")] Ejemplar ejemplar)
        {
            //if (ModelState.IsValid)
            {
                _context.Add(ejemplar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstanteId"] = new SelectList(_context.Estantes, "EstanteId", "EstanteId", ejemplar.EstanteId);
            ViewData["PublicacionId"] = new SelectList(_context.Publicaciones, "PublicacionId", "PublicacionId", ejemplar.PublicacionId);
            return View(ejemplar);
        }

        // GET: Ejemplares/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ejemplar = await _context.Ejemplares.FindAsync(id);
            if (ejemplar == null)
            {
                return NotFound();
            }
            ViewData["EstanteId"] = new SelectList(_context.Estantes, "EstanteId", "EstanteId", ejemplar.EstanteId);
            ViewData["PublicacionId"] = new SelectList(_context.Publicaciones, "PublicacionId", "PublicacionId", ejemplar.PublicacionId);
            return View(ejemplar);
        }

        // POST: Ejemplares/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("EjemplarId,PublicacionId,EstanteId")] Ejemplar ejemplar)
        {
            if (id != ejemplar.EjemplarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ejemplar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EjemplarExists(ejemplar.EjemplarId))
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
            ViewData["EstanteId"] = new SelectList(_context.Estantes, "EstanteId", "EstanteId", ejemplar.EstanteId);
            ViewData["PublicacionId"] = new SelectList(_context.Publicaciones, "PublicacionId", "PublicacionId", ejemplar.PublicacionId);
            return View(ejemplar);
        }

        // GET: Ejemplares/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ejemplar = await _context.Ejemplares
                .Include(e => e.Estante)
                .Include(e => e.Publicacion)
                .FirstOrDefaultAsync(m => m.EjemplarId == id);
            if (ejemplar == null)
            {
                return NotFound();
            }

            return View(ejemplar);
        }

        // POST: Ejemplares/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ejemplar = await _context.Ejemplares.FindAsync(id);
            if (ejemplar != null)
            {
                _context.Ejemplares.Remove(ejemplar);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EjemplarExists(int id)
        {
            return _context.Ejemplares.Any(e => e.EjemplarId == id);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
