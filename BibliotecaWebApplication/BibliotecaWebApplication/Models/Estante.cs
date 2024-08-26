using System.ComponentModel.DataAnnotations;

namespace BibliotecaWebApplication.Models
{
    public class Estante
    {
        [Key]
        public int EstanteId { get; set; }
        public string CodigoEstante { get; set; }
        // Fk Estanteria
        public int? EstanteriaId { get; set; }

        // Navigation properties
        public Estanteria Estanteria { get; set; }
        public ICollection<Ejemplar> Ejemplares { get; set; } = new List<Ejemplar>();
    }
}
