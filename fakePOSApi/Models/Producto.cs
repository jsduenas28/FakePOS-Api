using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakePOSApi.Models
{
    public class Producto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDProducto { get; set; }
        public string CodProducto { get; set; }
        public string Descripcion { get; set; }
        public int Stock { get; set; }
        public double Precio { get; set; }
        public int IDCategoria { get; set; }

        [ForeignKey("IDCategoria")]
        public Categoria Categoria { get; set; }
    }
}
