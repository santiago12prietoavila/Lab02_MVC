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
    public class EstanteriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EstanteriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Estanterias
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estanterias.ToListAsync());
        }

        // GET: Estanterias/Details/5
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estanteria = await _context.Estanterias
                .FirstOrDefaultAsync(m => m.EstanteriaId == id);
            if (estanteria == null)
            {
                return NotFound();
            }

            return View(estanteria);
        }

        // GET: Estanterias/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estanterias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstanteriaId,Alto,Ancho")] Estanteria estanteria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estanteria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estanteria);
        }

        // GET: Estanterias/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estanteria = await _context.Estanterias.FindAsync(id);
            if (estanteria == null)
            {
                return NotFound();
            }
            return View(estanteria);
        }

        // POST: Estanterias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(int id, [Bind("EstanteriaId,Alto,Ancho")] Estanteria estanteria)
        {
            if (id != estanteria.EstanteriaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estanteria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstanteriaExists(estanteria.EstanteriaId))
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
            return View(estanteria);
        }

        // GET: Estanterias/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estanteria = await _context.Estanterias
                .FirstOrDefaultAsync(m => m.EstanteriaId == id);
            if (estanteria == null)
            {
                return NotFound();
            }

            return View(estanteria);
        }

        // POST: Estanterias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estanteria = await _context.Estanterias.FindAsync(id);
            if (estanteria != null)
            {
                _context.Estanterias.Remove(estanteria);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstanteriaExists(int id)
        {
            return _context.Estanterias.Any(e => e.EstanteriaId == id);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
