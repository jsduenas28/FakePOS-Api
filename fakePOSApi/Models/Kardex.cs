using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fakePOSApi.Models
{
    public class Kardex
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDKardex { get; set; }
        public int IDProducto { get; set; }
        public string NumDocumento { get; set; }
        public string TipoMovimiento { get; set; }
        public int Entrada { get; set; }
        public int Salida { get; set; }
        public DateOnly Fecha { get; set; }
        public int IDUser { get; set; }

        [ForeignKey("IDProducto")]
        public Producto Producto { get; set; }

        [ForeignKey("IDUser")]
        public Usuario Usuario { get; set; }
    }
}
