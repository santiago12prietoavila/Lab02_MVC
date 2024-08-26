using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BibliotecaWebApplication.Models;
using Microsoft.AspNetCore.Identity;

namespace BibliotecaWebApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeo de tablas
            modelBuilder.Entity<Libro>().ToTable("Libros");
            modelBuilder.Entity<Revista>().ToTable("Revistas");

            // Configuración de claves compuestas para las tablas de identidad
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .HasKey(ul => new { ul.UserId, ul.LoginProvider, ul.ProviderKey });

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<IdentityUserToken<string>>()
                .HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });

            // Configuración de claves compuestas y relaciones para AutorLibro
            modelBuilder.Entity<AutorLibro>()
                .HasKey(al => new { al.AutorId, al.LibroId });

            modelBuilder.Entity<AutorLibro>()
                .HasOne(al => al.Autor)
                .WithMany(a => a.AutorLibros)
                .HasForeignKey(al => al.AutorId);

            modelBuilder.Entity<AutorLibro>()
                .HasOne(al => al.Libro)
                .WithMany(l => l.AutorLibros)
                .HasForeignKey(al => al.LibroId);

            // Configuración de la relación entre Ejemplar y Publicacion

            modelBuilder.Entity<Ejemplar>()
                .HasOne(e => e.Publicacion)
                .WithMany(p => p.Ejemplares)
                .HasForeignKey(e => e.PublicacionId);

            // Configuración de la relación entre Estante y Estanteria
            modelBuilder.Entity<Estante>()
                .HasOne(e => e.Estanteria)
                .WithMany(es => es.Estantes)
                .HasForeignKey(e => e.EstanteriaId);

            // Configuración de la relación entre Libro y Publicacion
            modelBuilder.Entity<Libro>()
                .HasOne(l => l.Publicacion)
                .WithOne()
                .HasForeignKey<Libro>(l => l.PublicacionId);

            // Configuración de la relación entre Revista y Publicacion
            modelBuilder.Entity<Revista>()
                .HasOne(r => r.Publicacion)
                .WithOne()
                .HasForeignKey<Revista>(r => r.PublicacionId);
        }

        // DbSets para cada entidad
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<AutorLibro> AutorLibros { get; set; }
        public DbSet<Ejemplar> Ejemplares { get; set; }
        public DbSet<Estante> Estantes { get; set; }
        public DbSet<Estanteria> Estanterias { get; set; }
        public DbSet<Revista> Revistas { get; set; }
        public DbSet<Publicacion> Publicaciones { get; set; }
        
    }
}
