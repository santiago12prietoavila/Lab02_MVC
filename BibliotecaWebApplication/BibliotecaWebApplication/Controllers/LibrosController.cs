using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BibliotecaWebApplication.Data;
using BibliotecaWebApplication.Models;
using Microsoft.AspNetCore.Authorization;

namespace BibliotecaWebApplication.Controllers
{
    public class LibrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Libros
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Index()
        {
            var libros = await _context.Libros
                .Include(l => l.AutorLibros)
                    .ThenInclude(al => al.Autor)
                .ToListAsync();
            return View(libros);
        }

        // GET: Libros/Details/5
        [Authorize(Roles = "Bibliotecario, Administrador")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.AutorLibros)
                    .ThenInclude(al => al.Autor)
                .FirstOrDefaultAsync(m => m.LibroId == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libros/Create
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            ViewBag.Autores = _context.Autores.ToList();
            return View();
        }

        // POST: Libros/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Create([Bind("LibroId,ISBN,Titulo,NumeroPaginas,Formato,PortadaUrl,ContraportadaUrl")] Libro libro, IFormFile portada, IFormFile contraportada, Guid[] selectedAutores)
        {
            //if (ModelState.IsValid)
            {
                var publicacion = new Publicacion
                {
                    PublicacionId = Guid.NewGuid()
                };

                _context.Publicaciones.Add(publicacion);
                await _context.SaveChangesAsync();

                libro.PublicacionId = publicacion.PublicacionId;
                libro.LibroId = Guid.NewGuid();
             
                _context.Add(libro);
                await _context.SaveChangesAsync();

                if (selectedAutores != null)
                {
                    foreach (var autorId in selectedAutores)
                    {
                        var autorLibro = new AutorLibro
                        {
                            AutorId = autorId,
                            LibroId = libro.LibroId
                        };
                        _context.AutorLibros.Add(autorLibro);
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Autores = _context.Autores.ToList();
            return View(libro);
        }



        // GET: Libros/Edit/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Publicacion)
                .Include(l => l.AutorLibros)
                    .ThenInclude(al => al.Autor)
                .FirstOrDefaultAsync(m => m.LibroId == id);

            if (libro == null)
            {
                return NotFound();
            }

            ViewBag.Autores = _context.Autores.ToList();
            ViewBag.SelectedAutores = libro.AutorLibros.Select(al => al.AutorId).ToList() ?? new List<Guid>();

            return View(libro);
        }

        // POST: Libros/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Edit(Guid id, [Bind("LibroId,ISBN,Titulo,NumeroPaginas,Formato")] Libro libro, IFormFile portada, IFormFile contraportada, Guid[] selectedAutores)
        {
            if (id != libro.LibroId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    var libroFromDb = await _context.Libros
                        .Include(l => l.Publicacion)
                        .FirstOrDefaultAsync(l => l.LibroId == id);

                    if (libroFromDb == null)
                    {
                        return NotFound();
                    }

                    libroFromDb.ISBN = libro.ISBN;
                    libroFromDb.Titulo = libro.Titulo;
                    libroFromDb.NumeroPaginas = libro.NumeroPaginas;
                    libroFromDb.Formato = libro.Formato;

                    

                    _context.Update(libroFromDb);
                    await _context.SaveChangesAsync();

                    var existingAutorLibros = _context.AutorLibros.Where(al => al.LibroId == id);
                    _context.AutorLibros.RemoveRange(existingAutorLibros);

                    if (selectedAutores != null && selectedAutores.Any())
                    {
                        foreach (var autorId in selectedAutores)
                        {
                            var autorLibro = new AutorLibro
                            {
                                AutorId = autorId,
                                LibroId = libro.LibroId
                            };
                            _context.AutorLibros.Add(autorLibro);
                        }
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.LibroId))
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

            ViewBag.Autores = _context.Autores.ToList();
            ViewBag.SelectedAutores = selectedAutores != null ? selectedAutores.ToList() : new List<Guid>();
            return View(libro);
        }


        private bool LibroExists(Guid id)
        {
            return _context.Libros.Any(e => e.LibroId == id);
        }



        // GET: Libros/Delete/5
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.AutorLibros)
                    .ThenInclude(al => al.Autor)
                .FirstOrDefaultAsync(m => m.LibroId == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var libro = await _context.Libros
                .Include(l => l.AutorLibros)
                .Include(l => l.Publicacion) // Incluye la Publicacion asociada
                .FirstOrDefaultAsync(l => l.LibroId == id);

            if (libro != null)
            {
                // Eliminar relaciones en AutorLibro
                _context.AutorLibros.RemoveRange(libro.AutorLibros);

                // Eliminar la publicacion asociada
                if (libro.Publicacion != null)
                {
                    _context.Publicaciones.Remove(libro.Publicacion);
                }

                // Eliminar el libro
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}

