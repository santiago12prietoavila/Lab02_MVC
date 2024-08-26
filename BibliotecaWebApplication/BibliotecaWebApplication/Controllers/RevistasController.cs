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
    public class RevistasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RevistasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Revistas
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Revistas.Include(r => r.Publicacion);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Revistas/Details/5
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revista = await _context.Revistas
                .Include(r => r.Publicacion)
                .FirstOrDefaultAsync(m => m.RevistaId == id);
            if (revista == null)
            {
                return NotFound();
            }

            return View(revista);
        }

        // GET: Revistas/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Revistas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("RevistaId,Numero,Nombre,FechaPublicacion")] Revista revista)
        {
            //if (ModelState.IsValid)
            {
                // Crear una nueva publicación
                var publicacion = new Publicacion
                {
                    PublicacionId = Guid.NewGuid()
                };

                // Guardar la publicación
                _context.Publicaciones.Add(publicacion);
                await _context.SaveChangesAsync();

                // Asignar la publicación a la revista
                revista.PublicacionId = publicacion.PublicacionId;
                revista.RevistaId = Guid.NewGuid();

                // Guardar la revista
                _context.Add(revista);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(revista);
        }

        // GET: Revistas/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revista = await _context.Revistas
                .Include(r => r.Publicacion)
                .FirstOrDefaultAsync(m => m.RevistaId == id);

            if (revista == null)
            {
                return NotFound();
            }

            ViewData["PublicacionId"] = new SelectList(_context.Publicaciones, "PublicacionId", "Nombre", revista.PublicacionId);
            return View(revista);
        }


        // POST: Revistas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(Guid id, [Bind("RevistaId,Numero,Nombre,FechaPublicacion")] Revista revista)
        {
            if (id != revista.RevistaId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    var revistaFromDb = await _context.Revistas
                        .Include(r => r.Publicacion) // Include related Publicacion
                        .FirstOrDefaultAsync(r => r.RevistaId == id);

                    if (revistaFromDb == null)
                    {
                        return NotFound();
                    }
                    revistaFromDb.Numero = revista.Numero;
                    revistaFromDb.Nombre = revista.Nombre;
                    revistaFromDb.FechaPublicacion = revista.FechaPublicacion;

                    _context.Update(revistaFromDb);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RevistaExists(revista.RevistaId))
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
            ViewData["PublicacionId"] = new SelectList(_context.Publicaciones, "PublicacionId", "Nombre", revista.PublicacionId);
            return View(revista);
        }


        // GET: Revistas/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var revista = await _context.Revistas
                .Include(r => r.Publicacion)
                .FirstOrDefaultAsync(m => m.RevistaId == id);
            if (revista == null)
            {
                return NotFound();
            }

            return View(revista);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var revista = await _context.Revistas
                .Include(r => r.Publicacion)
                .FirstOrDefaultAsync(r => r.RevistaId == id);

            if (revista != null)
            {
                // Eliminar la publicación asociada
                if (revista.Publicacion != null)
                {
                    _context.Publicaciones.Remove(revista.Publicacion);
                }

                // Eliminar la revista
                _context.Revistas.Remove(revista);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RevistaExists(Guid id)
        {
            return _context.Revistas.Any(e => e.RevistaId == id);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
