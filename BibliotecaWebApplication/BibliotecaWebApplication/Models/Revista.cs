using System.ComponentModel.DataAnnotations;

namespace BibliotecaWebApplication.Models
{
    public class Revista
    {
        [Key]
        public Guid RevistaId { get; set; }
        public string Numero { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaPublicacion { get; set; }

        // Clave foránea que referencia a Publicacion
        public Guid? PublicacionId { get; set; }
        public Publicacion Publicacion { get; set; }

        public Revista()
        {
            this.RevistaId = Guid.NewGuid(); // Generar automáticamente el ID de la revista
        }
    }

}
