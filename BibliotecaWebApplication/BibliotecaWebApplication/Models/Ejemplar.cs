using System.ComponentModel.DataAnnotations;

namespace BibliotecaWebApplication.Models
{
    public class Ejemplar
    {
        public int EjemplarId { get; set; }
        public Guid? PublicacionId { get; set; }
        public int? EstanteId { get; set; }

        //Navegacion de propiedades
        public Publicacion Publicacion { get; set; }
        public Estante Estante { get; set; }
    }
}
