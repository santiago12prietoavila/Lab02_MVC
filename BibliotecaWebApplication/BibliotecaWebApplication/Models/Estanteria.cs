using System.ComponentModel.DataAnnotations;

namespace BibliotecaWebApplication.Models
{
    public class Estanteria
    {
        [Key]
        public int EstanteriaId { get; set; }
        public double Alto { get; set; }
        public double Ancho { get; set; }

        // Navigation properties
        public ICollection<Estante> Estantes { get; set; } = new List<Estante>();
    }
}
